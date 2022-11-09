using PressDot.Contracts;
using PressDot.Contracts.Request.Customer;
using PressDot.Contracts.Response.Account;
using PressDot.Contracts.Response.Users;
using System.Collections.Generic;
using PressDot.Contracts.Request.Users;

namespace PressDot.Facade.Infrastructure
{
    public interface IUsersFacade
    {
        UsersRegistrationResponse RegisterUser(RegisterUsersRequest registerCustomerRequest);

        UserAuthenticationResponse AuthenticateUser(string email, string password);

        bool ChangePassword(int userId, string password, string oldPassword);

        bool ChangePassword(string token, string password);

        bool ActivateAccount(string token);

        bool ForgotPassword(string email);

        ICollection<UsersResponse> GetUsers();

        PressDotPageListEntityModel<UsersResponse> GetUsers(string name, int? userRoleId, int pageIndex, int pageSize);
        /// <summary>
        /// Gets and deletes user by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteUser(int userId);
        /// <summary>
        /// Returns users by role id and user name as if supplied i.e optional
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        ICollection<UsersResponse> GetUsersByRoles(int roleId, string userName = "");

        ICollection<UsersResponse> GetDentistUsers();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>User Response Object</returns>
        UsersResponse GetUserById(int userId);

        bool UpdateUser(UserUpdateRequest request);

        ICollection<UserDevicesResponse> GetUserDevicesByUserId(int userId);

        bool DeleteAllUserDeviceToken(int userId);

        UserDevicesResponse SaveUserDevice(UserDeviceRequest userDeviceRequest);

        UserDevicesResponse GetUserDeviceByUserIdAndDeviceId(int userId, string deviceId);
    }
}
