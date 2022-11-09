
using PressDot.Contracts;
using PressDot.Contracts.Request.Appointment;
using PressDot.Contracts.Response.Appointment;
using PressDot.Core.Domain;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PressDot.Contracts.Model;
using PressDot.Contracts.Response.State;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.RazorRenderer;
using System.Collections.Generic;

namespace PressDot.Facade.Implementation
{
    public class AppointmentFacade : IAppointmentFacade
    {
        #region private

        private readonly IAppointmentService _appointmentService;
        private readonly IUsersService _usersService;
        private readonly ISaloonService _saloonService;
        private readonly IStatesService _statesService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRendererService;
        private readonly IEmailService _emailService;
        private readonly ISaloonEmployeeService _saloonEmployeeService;
        private readonly IUserDevicesService _userDevicesService;
        private readonly IMessagingService _messagingService;
        private readonly INotificationService _notificationService;
        private readonly ILaboratoryUsersService _laboratoryUsersService;
        private readonly ISaloonLaboratoryService _saloonLaboratoryService;
        #endregion


        #region ctor

        public AppointmentFacade(ISaloonLaboratoryService saloonLaboratoryService, ILaboratoryUsersService laboratoryUsersService, INotificationService notificationService, IAppointmentService appointmentService, IUsersService usersService, ISaloonService saloonService
            , IStatesService statesService, IRazorPartialToStringRenderer razorPartialToStringRendererService
            , IEmailService emailService, ISaloonEmployeeService saloonEmployeeService
            ,IUserDevicesService userDevicesService, IMessagingService messagingService)
        {
            _appointmentService = appointmentService;
            _usersService = usersService;
            _saloonService = saloonService;
            _statesService = statesService;
            _razorPartialToStringRendererService = razorPartialToStringRendererService;
            _emailService = emailService;
            _saloonEmployeeService = saloonEmployeeService;
            _userDevicesService = userDevicesService;
            _messagingService = messagingService;
            _notificationService = notificationService;
            _laboratoryUsersService = laboratoryUsersService;
            _saloonLaboratoryService = saloonLaboratoryService;
        }
        #endregion

        #region methods
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsbySaloonId(Id, pageIndex, pageSize);
            if (appointmentRes == null) return null;
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, DateTime date, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsbySaloonId(Id, date, pageIndex, pageSize);
            if (appointmentRes == null) return null;
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyDoctorId(int Id, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsbyDoctorId(Id, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public AppointmentResponse GetAppointmentById(int id)
        {
            var appointmentRes = _appointmentService.Get(id);

            if (appointmentRes == null) return null;
            appointmentRes.Customer = _usersService.Get(appointmentRes.CustomerId);
            appointmentRes.Doctor = _usersService.Get(appointmentRes.DoctorId);
            appointmentRes.Saloon = _saloonService.Get(appointmentRes.SaloonId);
            appointmentRes.State = _statesService.Get(appointmentRes.StateId);
            return appointmentRes.ToModel<AppointmentResponse>(); ;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyCustomerId(int Id, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsbyCustomerId(Id, pageIndex, pageSize);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentsbyAppointmentStates(AppointmentStateRequest request, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsbyAppointmentState(request.states, pageIndex, pageSize);
            if (appointmentRes == null) return null;
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }

        public PressDotPageListEntityModel<AppointmentResponse> GetSaloonAppointmentsForSaloonAdministrator(AppointmentStateRequest request,int userId, int pageIndex, int pageSize)
        {
            var saloonEmployee = _saloonEmployeeService.GetSaloonEmployeeByUserId(userId);
            if (saloonEmployee == null)
                return null;
            var appointmentRes = _appointmentService.GetSaloonAppointmentsForSaloonAdministrator(request.states,saloonEmployee.SaloonId, pageIndex, pageSize);
            if (appointmentRes == null) return null;

            var customers = _usersService.Get(appointmentRes.Select(x => x.CustomerId).ToArray());
            var doctors = _usersService.Get(appointmentRes.Select(x => x.DoctorId).ToArray());
            var states = _statesService.Get(appointmentRes.Select(x => x.StateId).ToArray());
            var saloons = _saloonService.Get(appointmentRes.Select(x => x.SaloonId).ToArray());

            foreach (var saloonAppointment in appointmentRes)
            {
                saloonAppointment.Doctor = doctors.FirstOrDefault(x => x.Id == saloonAppointment.DoctorId);
                saloonAppointment.Customer = customers.FirstOrDefault(x => x.Id == saloonAppointment.CustomerId);
                saloonAppointment.State = states.FirstOrDefault(x => x.Id == saloonAppointment.StateId);
                saloonAppointment.Saloon = saloons.FirstOrDefault(x => x.Id == saloonAppointment.SaloonId);
            }

            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }


        public bool UpdateAppointment(AppointmentRequest request)
        {
            var appointment = request.ToEntity<Appointment>();
            return _appointmentService.UpdateAppointment(appointment);
        }
        public bool UpdateAppointmentState(int id, int stateId)
        {
            var result = _appointmentService.UpdateAppointmentState(id, stateId);
            var appointmentRes = GetAppointmentById(id);
            var checkNotification = _notificationService.CheckMadeNotification((int)appointmentRes.CustomerId);
            if (checkNotification.IsAppointmentCompletedStatus)
            {
                if (appointmentRes != null)
                {
                    if (appointmentRes.State.Value.Equals("Approved"))
                    {
                        var appointmentModelForEmail = new AppointmentInfoToCustomer
                        {
                            AppointmentId = appointmentRes.Id,
                            Firstname = appointmentRes.Customer.Firstname,
                            Lastname = appointmentRes.Customer.Lastname,
                            StartTimeString = appointmentRes.StartTimeString,
                            EndTimeString = appointmentRes.EndTimeString,
                            State = appointmentRes.State.Value
                        };
                        var userDevices = _userDevicesService.GetUserDevicesByUserId(appointmentRes.CustomerId.Value);
                        foreach (var userDevice in userDevices)
                        {
                            _messagingService.SendNotification(userDevice.DeviceToken,
                                $"Dear {appointmentRes.Customer.Firstname}",
                                $"Your appointment is confirmed for {appointmentRes.Date.Value.ToString("D")} at {appointmentRes.StartTimeString}", userDevice.UserId);
                        }

                        var html = _razorPartialToStringRendererService.RenderPartialToStringAsync(
                            "_AppointmentInfoToCustomer",
                            appointmentModelForEmail);
                        //Send DENTIST
                        _emailService.SendEmail("PressDot Appointment Status", html.Result, appointmentRes.Doctor.Email);
                        //Send To Saloon Admin
                        _emailService.SendEmail("PressDot Appointment Status", html.Result, appointmentRes.Saloon.Email);
                        //Send Customer
                        _emailService.SendEmail("PressDot Appointment Status", html.Result, appointmentRes.Customer.Email);
                    }

                    if (appointmentRes.State.Value.Equals("Reject"))
                    {
                        var userDevices = _userDevicesService.GetUserDevicesByUserId(appointmentRes.CustomerId.Value);
                        foreach (var userDevice in userDevices)
                        {
                            _messagingService.SendNotification(userDevice.DeviceToken,
                                $"Dear {appointmentRes.Customer.Firstname}",
                                $"Your appointment is rejected for {appointmentRes.Date.Value.ToString("D")} at {appointmentRes.StartTimeString}", userDevice.UserId);
                        }
                    }
                }
            }
            return result;
        }
        public bool CreateAppointment(AppointmentRequest request)
        {
            var appointment = request.ToEntity<Appointment>();
            var result = _appointmentService.CreateAppointment(appointment);
            var checkNotification = _notificationService.CheckMadeNotification((int)request.CustomerId);
            if (checkNotification.IsAppointmentMadeStatus)
            {
                var appointmentRes = GetAppointmentById(result.Id);
                if (appointmentRes != null)
                {
                    var appointmentModelForEmail = new AppointmentInfoToCustomer
                    {
                        AppointmentId = appointmentRes.Id,
                        Firstname = appointmentRes.Customer.Firstname,
                        Lastname = appointmentRes.Customer.Lastname,
                        StartTimeString = appointmentRes.StartTimeString,
                        EndTimeString = appointmentRes.EndTimeString,
                        State = appointmentRes.State.Value
                    };
                    var html = _razorPartialToStringRendererService.RenderPartialToStringAsync("_AppointmentInfoToCustomer",
                        appointmentModelForEmail);

                    /// Sending Notification to Saloon Administrator
                    var saloonAdministrators = _saloonEmployeeService.GetSaloonAdministrators(request.SaloonId.Value);
                    foreach (var saloonAdministrator in saloonAdministrators)
                    {
                        var userDevices = _userDevicesService.GetUserDevicesByUserId(saloonAdministrator.EmployeeId);
                        var user = _usersService.Get(saloonAdministrator.EmployeeId);
                        foreach (var userDevice in userDevices)
                        {
                            _messagingService.SendNotification(userDevice.DeviceToken,
                                $"Dear {user.Firstname}",
                                $"New appointment is created with below detail.\r\n" +
                                $"Patient: {appointmentRes.Customer.Firstname} {appointmentRes.Customer.Lastname}" +
                                $"Phone: {appointmentRes.Customer.MobileNumber}" +
                                $"Date: {appointmentRes.Date.Value.ToString("D")}\r\n" +
                                $"Time: {appointmentRes.StartTimeString} to {appointmentRes.EndTimeString}", userDevice.UserId);
                        }
                    }

                    /// Sending Notification to Saloon Doctor
                    var doctorDevices = _userDevicesService.GetUserDevicesByUserId(request.DoctorId.Value);
                    var doctor = _usersService.Get(request.DoctorId.Value);
                    foreach (var userDevice in doctorDevices)
                    {
                        _messagingService.SendNotification(userDevice.DeviceToken,
                            $"Dear {doctor.Firstname}",
                            $"New appointment is created with below detail.\r\n" +
                            $"Patient: {appointmentRes.Customer.Firstname} {appointmentRes.Customer.Lastname}" +
                            $"Phone: {appointmentRes.Customer.MobileNumber}" +
                            $"Date: {appointmentRes.Date.Value.ToString("D")}\r\n" +
                            $"Time: {appointmentRes.StartTimeString} to {appointmentRes.EndTimeString}", userDevice.UserId);
                    }

                    _emailService.SendEmail("PressDot Appointment Status", html.Result, appointmentRes.Customer.Email);
                }
            }
            return true;
        }
        public PressDotPageListEntityModel<CurrentUserAppointmentResponse> GetCurrentUserAppointments(int customerId, bool isFutureAppointments, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetCurrentUserAppointments(customerId, isFutureAppointments, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            
            var appointments = new PressDotPageListEntityModel<CurrentUserAppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<CurrentUserAppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            foreach (var item in appointments.Data)
            {
                var appointmentTime = item.Date;
                appointmentTime = appointmentTime.Value.Add(item.StartTime.Value);
                if (appointmentTime.Value.Subtract(DateTime.Now).TotalHours > 24)
                    item.CanCancel = true;
                var saloon = new Saloon();
                var state = new States();
                var doctor = new Users();
                if (item.SaloonId != null)
                {
                    saloon = _saloonService.GetSaloonById(item.SaloonId.Value);
                }

                if (item.StateId != null)
                {
                    state = _statesService.Get(item.StateId.Value);
                }
                if (item.DoctorId != null)
                {
                    doctor = _usersService.Get(item.DoctorId.Value);
                }

                if (saloon != null)
                {
                    item.SaloonName = saloon.SaloonName;
                    item.SaloonAddress = saloon.Address;
                    item.SaloonEmail = saloon.Email;
                    item.SaloonPhone = saloon.Phone;
                }

                if (doctor != null)
                {
                    item.DoctorFirstName = doctor.Firstname;
                    item.DoctorLastName = doctor.Lastname;
                }
                if (state != null)
                {
                    item.State = state.ToModel<StateResponse>();
                }
            }

            
            appointments.Data = appointments.Data.OrderBy(x => x.Date).ToList();
            return appointments;
        }

        /// <summary>
        /// Get appointments by saloon administrator
        /// </summary>
        /// <param name="saloonAdministatorId">Saloon Administrator Id</param>
        /// <param name="pageIndex">page number </param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonAdministratorId(int saloonAdministatorId, int stateId, int pageIndex,
            int pageSize)
        {
            var saloonEmployee = _saloonEmployeeService.GetSaloonEmployeeByUserId(saloonAdministatorId);
            if(saloonEmployee == null)
                throw new PressDotException((int)AppointmentExceptionsCodes.NoSaloonAssociatedWithUser, "No saloon is associated with this user.");
            
            var saloonAppointments = _appointmentService.GetAppointmentsbySaloonIdAndState(saloonEmployee.SaloonId,stateId,pageIndex,pageSize);
            if (saloonAppointments == null) return null;

           var customers = _usersService.Get(saloonAppointments.Select(x=>x.CustomerId).ToArray());
           var doctors = _usersService.Get(saloonAppointments.Select(x => x.DoctorId).ToArray());
           var states = _statesService.Get(saloonAppointments.Select(x => x.StateId).ToArray());
           var saloons = _saloonService.Get(saloonAppointments.Select(x => x.SaloonId).ToArray());

           foreach (var saloonAppointment in saloonAppointments)
           {
               saloonAppointment.Doctor = doctors.FirstOrDefault(x => x.Id == saloonAppointment.DoctorId);
               saloonAppointment.Customer = customers.FirstOrDefault(x => x.Id == saloonAppointment.CustomerId);
               saloonAppointment.State = states.FirstOrDefault(x => x.Id == saloonAppointment.StateId);
               saloonAppointment.Saloon = saloons.FirstOrDefault(x => x.Id == saloonAppointment.SaloonId);
           }

            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = saloonAppointments.TotalCount,
                Data = saloonAppointments.ToModel<AppointmentResponse>(),
                HasNextPage = saloonAppointments.HasNextPage,
                HasPreviousPage = saloonAppointments.HasPreviousPage,
                TotalPages = saloonAppointments.TotalPages,
                PageIndex = saloonAppointments.PageIndex,
                PageSize = saloonAppointments.PageSize
            };
            return appointments;
        }

        public PressDotPageListEntityModel<AppointmentResponse> GetCompleteAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize)
        {
            if (userId != 0)
            {
                var appointmentRes = _appointmentService.GetAppointmentsbyCompleted(userId, stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;
                
            }
            else
            {
                var appointmentRes = _appointmentService.GetAppointmentsActiveForAdmin(stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;


            }
        }

        public PressDotPageListEntityModel<AppointmentResponse> GetCancelAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize)
        {
            if (userId != 0)
            {
                var appointmentRes = _appointmentService.GetAppointmentsbyCancel(userId,stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;

            }
            else
            {
                var appointmentRes = _appointmentService.GetAppointmentsCancelForAdmin(stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;
            }
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetRejectAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize)
        {
            if (userId != 0)
            {
                var appointmentRes = _appointmentService.GetAppointmentsbyReject(userId,stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;

            }
            else
            {
                var appointmentRes = _appointmentService.GetAppointmentsRejectForAdmin(stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;
            }
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetPendingAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize)
        {
            if (userId != 0)
            {
                var appointmentRes = _appointmentService.GetAppointmentsbyPending(userId,stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;

            }
            else
            {
                var appointmentRes = _appointmentService.GetAppointmentsPendingForAdmin(stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;
            }
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetApprovedAppointmentForDashboard(int userId, int stateId,string search, int pageIndex, int pageSize)
        {
            if (userId != 0)
            {
                var appointmentRes = _appointmentService.GetAppointmentsbyApproved(userId,stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;

                
                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;

            }
            else
            {
                var appointmentRes = _appointmentService.GetAppointmentsApprovedForAdmin(stateId, pageIndex, pageSize);
                if (appointmentRes == null) return null;
               

                foreach (var item in appointmentRes)
                {
                    item.Customer = _usersService.Get(item.CustomerId);
                    item.Doctor = _usersService.Get(item.DoctorId);
                    item.Saloon = _saloonService.Get(item.SaloonId);
                    item.State = _statesService.Get(item.StateId);
                }
                var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
                {
                    TotalCount = appointmentRes.TotalCount,
                    Data = appointmentRes.ToModel<AppointmentResponse>(),
                    HasNextPage = appointmentRes.HasNextPage,
                    HasPreviousPage = appointmentRes.HasPreviousPage,
                    TotalPages = appointmentRes.TotalPages,
                    PageIndex = appointmentRes.PageIndex,
                    PageSize = appointmentRes.PageSize
                };
                return appointments;
            }
        }

        public List<ProducatResponse> LoadProducts()
        {
            var list = new List<ProducatResponse>();
            list.Add(new ProducatResponse()
            {
              id= "1000",
             code = "f230fh0g3",
            name= "Opalescence Whitening Gel 16% CP 8 Syringes",
            description= "Product Description",
            image= "bamboo-watch.jpg",
            price= 65,
            category= "Accessories",
            quantity= 24,
            inventoryStatus= "INSTOCK",
           rating= 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Opalescence Whitening Gel 16% CP 8 Syringes",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            list.Add(new ProducatResponse()
            {
                id = "1000",
                code = "f230fh0g3",
                name = "Bamboo Watch",
                description = "Product Description",
                image = "bamboo-watch.jpg",
                price = 65,
                category = "Accessories",
                quantity = 24,
                inventoryStatus = "INSTOCK",
                rating = 5
            });
            return list;
        }


        public List<Appointment> GetPendingAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize)
        {
            
         
            var appointmentRes = _appointmentService.GetAppointmentPendingForLab(userId,0 ,stateId);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            
            return appointmentRes;
        }
        
        public List<Appointment> GetApprovedAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize)
        {
            
         
            var appointmentRes = _appointmentService.GetAppointmentsApproveForLab(userId,0 ,stateId);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            
            return appointmentRes;
        }    
        public List<Appointment> GetRejectAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize)
        {
            
         
            var appointmentRes = _appointmentService.GetAppointmentsRejectForLab(userId,0 ,stateId);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            
            return appointmentRes;
        }    
        public List<Appointment> GetCancelAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize)
        {
            
         
            var appointmentRes = _appointmentService.GetAppointmentsCancelForLab(userId,0 ,stateId);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            
            return appointmentRes;
        }

        public List<Appointment> GetActiveAppointmentForLabApprove(int userId, int stateId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsActiveForLabApprove(userId, 0, stateId);
            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }

            return appointmentRes;
        }

        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentApprovedbyDoctorId(int Id, int statusId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsApprovebyDoctorId(Id,statusId ,pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }

        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentActivebyDoctorId(int Id, int statusId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsApprovebyDoctorId(Id,statusId, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        } 
        
        
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentPendingbyDoctorId(int Id, int statusId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsPendingbyDoctorId(Id,statusId, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentRejectbyDoctorId(int Id, int statusId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsRejectbyDoctorId(Id,statusId, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        public PressDotPageListEntityModel<AppointmentResponse> GetAppointmentCancelbyDoctorId(int Id, int statusId, int pageIndex, int pageSize)
        {
            var appointmentRes = _appointmentService.GetAppointmentsCancelbyDoctorId(Id,statusId, pageIndex, pageSize);

            if (appointmentRes == null) return null;
            foreach (var item in appointmentRes)
            {
                item.Customer = _usersService.Get(item.CustomerId);
                item.Doctor = _usersService.Get(item.DoctorId);
                item.Saloon = _saloonService.Get(item.SaloonId);
                item.State = _statesService.Get(item.StateId);
            }
            var appointments = new PressDotPageListEntityModel<AppointmentResponse>()
            {
                TotalCount = appointmentRes.TotalCount,
                Data = appointmentRes.ToModel<AppointmentResponse>(),
                HasNextPage = appointmentRes.HasNextPage,
                HasPreviousPage = appointmentRes.HasPreviousPage,
                TotalPages = appointmentRes.TotalPages,
                PageIndex = appointmentRes.PageIndex,
                PageSize = appointmentRes.PageSize
            };
            return appointments;
        }
        #endregion
    }
}
