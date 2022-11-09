using Microsoft.IdentityModel.Tokens;
using PressDot.Contracts;
using PressDot.Contracts.Model;
using PressDot.Contracts.Request.Customer;
using PressDot.Contracts.Response.Account;
using PressDot.Contracts.Response.Users;
using PressDot.Core;
using PressDot.Core.Configuration;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Framework.RazorRenderer;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using PressDot.Contracts.Request.Users;

namespace PressDot.Facade.Implementation
{
    public class UsersFacade : IUsersFacade
    {
        #region private

        private readonly IUsersService _usersService;

        private readonly PressDotConfig _pressDotConfig;

        private readonly IEmailService _emailService;

        private readonly IUserRoleService _userRoleService;

        private readonly IRazorPartialToStringRenderer _razorPartialToStringRendererService;

        private readonly IStatesService _statesService;

        private readonly IAppointmentService _appointmentService;

        private readonly IUserDevicesService _userDevicesService;

        private readonly ISaloonEmployeeService _saloonEmployeeService;
        private readonly IConsentService _consentService;
        private readonly INotificationService _notificationService;
        #endregion

        #region ctor

        public UsersFacade(INotificationService notificationService, IConsentService consentService,IUsersService usersService, PressDotConfig pressDotConfig, IEmailService emailService
            , IUserRoleService userRoleService, IRazorPartialToStringRenderer razorPartialToStringRendererService
            , IStatesService statesService, IAppointmentService appointmentService,IUserDevicesService userDevicesService
            , ISaloonEmployeeService saloonEmployeeService)
        {
            _usersService = usersService;
            _pressDotConfig = pressDotConfig;
            _emailService = emailService;
            _userRoleService = userRoleService;
            _razorPartialToStringRendererService = razorPartialToStringRendererService;
            _statesService = statesService;
            _appointmentService = appointmentService;
            _userDevicesService = userDevicesService;
            _saloonEmployeeService = saloonEmployeeService;
            _consentService= consentService;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        public UsersRegistrationResponse RegisterUser(RegisterUsersRequest registerCustomerRequest)
        {
            if (registerCustomerRequest == null)
                throw new PressDotValidationException(1000, "Fill customer basic information.");
            if (string.IsNullOrEmpty(registerCustomerRequest.Firstname))
                throw new PressDotValidationException(1001, "First name is mandatory.");
            if (string.IsNullOrEmpty(registerCustomerRequest.Lastname))
                throw new PressDotValidationException(1002, "Last name is mandatory.");
            if (string.IsNullOrEmpty(registerCustomerRequest.Email))
                throw new PressDotValidationException(1003, "Email is mandatory.");
            if (string.IsNullOrEmpty(registerCustomerRequest.MobileNumber))
                throw new PressDotValidationException(1004, "Mobile number is mandatory.");
            if (string.IsNullOrEmpty(registerCustomerRequest.Password))
                throw new PressDotValidationException(1005, "Password is mandatory.");

            if (_usersService.UserExist(registerCustomerRequest.Email))
            {
                throw new PressDotAlreadyExistException(1006, "Email is already used by another customer.");
            }

            var user = registerCustomerRequest.ToEntity<Users>();
            user.CreatedDate = DateTime.Now;
            var salt = CommonHelper.RandomString(_pressDotConfig.PasswordSaltLength);
            user.Password = CommonHelper.ComputeHash(user.Password, salt);
            user.PasswordSalt = salt;
            _usersService.Create(user);

            var model = new NewRegistrationModel()
            {
                Firstname = user.Firstname,
                Token = CommonHelper.Encrypt(user.Id.ToString(CultureInfo.InvariantCulture) + "@" +
                                             DateTime.Now.ToString("MM/dd/yyyy HH:mm")),
                WebsiteUrl = _pressDotConfig.WebsiteUrl,
                UserRoleId = registerCustomerRequest.UserRoleId,
                SelfRegistration = registerCustomerRequest.SelfRegistration

            };
            var consent = new Consent()
            {
                IsSent = false,
                UserId = user.Id
            };
            _consentService.CreateConsent(consent);
            var userRole = GetUserById(user.Id);
            if (userRole.UsersRole.UserRoleName == "Customer")
            {
                var listnotification = new List<Notification>();
                listnotification.Add(new Notification()
                {
                    IsAppointmentCompletedStatus = true,
                    IsAppointmentMadeStatus = true,
                    Type = 1,
                    UserId = user.Id
                });
                listnotification.Add(new Notification()
                {
                    IsAppointmentCompletedStatus = true,
                    IsAppointmentMadeStatus = true,
                    Type = 2,
                    UserId = user.Id
                });
                _notificationService.CreateNotification(listnotification);

            }
            var html = _razorPartialToStringRendererService.RenderPartialToStringAsync("_NewRegistration", model);
           _emailService.SendEmail("PressDots Registration", html.Result, user.Email);

            return user.ToModel<UsersRegistrationResponse>();
        }

        public UserAuthenticationResponse AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new PressDotValidationException(1007, "Username is required.");
            if (string.IsNullOrWhiteSpace(password))
                throw new PressDotValidationException(1008, "Password is required.");
            var user = _usersService.GetUserByEmail(email);
            if (user == null)
                return null;
            var passwordHash = CommonHelper.ComputeHash(password, user.PasswordSalt);
            if (!user.Password.Equals(passwordHash))
                return null;
            if (!user.IsActive)
                throw new PressDotValidationException(1009, "User is not activated.");

            user.UsersRole = _userRoleService.Get(user.UserRoleId);
            var token = GenerateJwtToken(user);
            return new UserAuthenticationResponse()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Email,
                UserRoleId = user.UserRoleId,
                UserRole = user.UsersRole.UserRoleName,
                Token = token
            };
        }

        public bool ChangePassword(int userId, string password, string oldPassword)
        {
            var user = _usersService.Get(userId);
            if (user == null)
                throw new PressDotNotFoundException(1009, "Invalid user id to change password.");
            var userOldPassword = CommonHelper.ComputeHash(oldPassword, user.PasswordSalt);
            if (!userOldPassword.Equals(user.Password))
                throw new PressDotValidationException(1016, "Invalid old password.");
            var salt = CommonHelper.RandomString(_pressDotConfig.PasswordSaltLength);
            user.Password = CommonHelper.ComputeHash(password, salt);
            user.PasswordSalt = salt;
            return _usersService.Update(user);
        }

        public bool ChangePassword(string token, string password)
        {
            var originalValue = CommonHelper.Decrypt(token);
            var tokenArray = originalValue.Split(new[] { '@' });
            if (tokenArray == null)
                throw new PressDotNotFoundException(1013, "Invalid token to change password.");
            int.TryParse(tokenArray[0], out int userId);
            var user = _usersService.Get(userId);
            if (user == null)
                throw new PressDotNotFoundException(1014, "Invalid token id to change password.");
            var salt = CommonHelper.RandomString(_pressDotConfig.PasswordSaltLength);
            user.Password = CommonHelper.ComputeHash(password, salt);
            user.PasswordSalt = salt;
            return _usersService.Update(user);
        }

        public bool ActivateAccount(string token)
        {
            var originalValue = CommonHelper.Decrypt(token);
            var tokenArray = originalValue.Split(new[] { '@' });
            if (tokenArray == null)
                throw new PressDotNotFoundException(1010, "Invalid token to activate user.");
            int.TryParse(tokenArray[0], out int userId);
            var user = _usersService.Get(userId);
            if (user == null)
                throw new PressDotNotFoundException(1011, "Invalid token id to activate user.");
            user.IsActive = true;
            return _usersService.Update(user);
        }


        public bool ForgotPassword(string email)
        {
            var user = _usersService.GetUserByEmail(email);

            if (user == null)
                throw new PressDotValidationException(1012, "No entry found against given email.");
            var model = new ForgotPasswordModel()
            {
                Firstname = user.Firstname,
                Token = CommonHelper.Encrypt(user.Id.ToString(CultureInfo.InvariantCulture) + "@" + DateTime.Now.ToString("MM/dd/yyyy HH:mm")),
                WebsiteUrl = _pressDotConfig.WebsiteUrl

            };
            var html = _razorPartialToStringRendererService.RenderPartialToStringAsync("_ForgotPassword", model);
            _emailService.SendEmail("PressDots Forgot Password", html.Result, user.Email);
            return true;
        }

        public ICollection<UsersResponse> GetUsers()
        {
            var users = _usersService.Get();
            if (users == null)
                throw new PressDotNotFoundException(1015, $"No user exist in system.");
            var roles = _userRoleService.Get(users.Select(x => x.UserRoleId).ToArray());
            foreach (var user in users)
            {
                user.UsersRole = roles.FirstOrDefault(x => x.Id == user.UserRoleId);
            }

            return users.ToModel<UsersResponse>();
        }

        public PressDotPageListEntityModel<UsersResponse> GetUsers(string name, int? userRoleId, int pageIndex, int pageSize)
        {
            var users = _usersService.GetUsers(name, userRoleId, pageIndex, pageSize);
            var roles = _userRoleService.Get(users.Select(x => x.UserRoleId).ToArray());
            foreach (var user in users)
            {
                user.UsersRole = roles.FirstOrDefault(x => x.Id == user.UserRoleId);
            }
            var userList = new PressDotPageListEntityModel<UsersResponse>()
            {
                TotalCount = users.TotalCount,
                Data = users.ToModel<UsersResponse>(),
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage,
                TotalPages = users.TotalPages,
                PageIndex = users.PageIndex,
                PageSize = users.PageSize

            };
            return userList;
        }

        public bool DeleteUser(int userId)
        {
            var stateIds = _statesService.GetStates().Where(s => s.StateFor == "Appointment" && s.Value == "Pending")
                .Select(s => s.Id).ToArray();

            var user = _usersService.Get(userId);

            if (user != null)
            {
                //Don't let user delete if system finds any appointments in pending state against the user.
                var isDeletable = _appointmentService.GetAppointmentsByUserAndSates(user.Id, stateIds).Any() == false;

                if (isDeletable)
                {
                    var saloonEmployees = _saloonEmployeeService.GetSaloonEmployeesByEmployeeId(user.Id);
                    foreach (var saloonEmployee in saloonEmployees)
                    {
                        _saloonEmployeeService.DeleteSaloonEmployeeAssociation(saloonEmployee);
                    }
                    
                    return _usersService.DeleteUser(user);
                }
                else
                {
                    throw new PressDotException((int)UserExceptionsCodes.Pending_Appointments_Found_For_User, "Unable to delete, pending appointments found against user.");
                }
            }
            else
            {
                throw new PressDotException((int)UserExceptionsCodes.User_Not_Found, "User not found.");
            }
        }

        public ICollection<UsersResponse> GetUsersByRoles(int roleId, string userName = "")
        {
            var checkRole = _userRoleService.IsValidRoleId(roleId);
            if (checkRole)
            {
                var users = _usersService.GetUsersByRoleId(roleId, userName);
                return users.ToModel<UsersResponse>();

            }
            else
            {
                throw new PressDotException((int)UserExceptionsCodes.Invalid_Role_Id, "Invalid role id.");
            }

        }

        public ICollection<UsersResponse> GetDentistUsers()
        {
            var users = _usersService.GetDentistUsers();
            return users.ToModel<UsersResponse>();

        }

        public UsersResponse GetUserById(int userId)
        {
            var user = _usersService.Get(userId);
            if (user != null && user.UserRoleId > 0)
                user.UsersRole = _userRoleService.Get(user.UserRoleId);
            return user?.ToModel<UsersResponse>();
        }

        public bool UpdateUser(UserUpdateRequest request)
        {
            if (request == null) throw new PressDotException("Invalid request to update user information.");
            var userModel = request.ToEntity<Users>();

            var user = _usersService.Get(userModel.Id);
            if(user == null)
                throw new PressDotException("User record does not exist in system.");
            user.Firstname = userModel.Firstname;
            user.Lastname = userModel.Lastname;
            user.Email = userModel.Email;
            user.MobileNumber = userModel.MobileNumber;
            user.IsActive = userModel.IsActive;
            if (userModel.UserRoleId > 0)
                user.UserRoleId = userModel.UserRoleId;
            user.UpdatedDate = DateTime.Now;
            return _usersService.Update(user);
        }

        public ICollection<UserDevicesResponse> GetUserDevicesByUserId(int userId)
        {
            var userDevices = _userDevicesService.GetUserDevicesByUserId(userId);
            if (userDevices == null)
                return null;
            return userDevices.ToModel<UserDevicesResponse>();
        }

        public UserDevicesResponse GetUserDeviceByUserIdAndDeviceId(int userId, string deviceId)
        {
            var userDevice = _userDevicesService.GetUserDeviceByUserIdAndDeviceId(userId, deviceId);
            if (userDevice == null)
                return null;
            return userDevice.ToModel<UserDevicesResponse>();
        }

        public bool DeleteAllUserDeviceToken(int userId)
        {
            return _userDevicesService.DeleteAllUserDeviceToken(userId);
        }


        public UserDevicesResponse SaveUserDevice(UserDeviceRequest userDeviceRequest)
        {
            var userDevice = userDeviceRequest.ToEntity<UserDevices>();
            _userDevicesService.Create(userDevice);
            return userDevice.ToModel<UserDevicesResponse>();
        }

        #endregion

        #region private methods

        private string GenerateJwtToken(Users user)
        {
            // generate token that is valid for 2 hours
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_pressDotConfig.SecretKeyForToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString())
                        , new Claim("firstname", user.Firstname)
                        , new Claim("lastname", user.Lastname)
                        , new Claim("email", user.Email)
                        , new Claim("isActive", user.IsActive.ToString())
                        , new Claim("userRoleId", user.UserRoleId.ToString())
                        , new Claim("userRole", user.UsersRole.UserRoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}