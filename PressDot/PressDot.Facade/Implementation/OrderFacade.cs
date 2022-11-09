
using System.Globalization;
using PressDot.Contracts;
using PressDot.Contracts.Request.Order;
using PressDot.Facade.Framework.Extensions;
using PressDot.Service.Infrastructure;
using PressDot.Contracts.Response.Order;
using PressDot.Contracts.Response.State;
using PressDot.Core.Domain;
using PressDot.Facade.Infrastructure;

namespace PressDot.Facade.Implementation
{
    public class OrderFacade : IOrderFacade
    {
        #region private

        private readonly IOrderService _orderService;
        private readonly IAppointmentService _appointmentService;
        private readonly IStatesService _statesService;
        private readonly ILaboratoryService _laboratoryService;
        private readonly IAppointmentFacade _appointmentFacade;
        private readonly ILaboratoryUsersService _laboratoryUsersService;
        private readonly IUserDevicesService _userDevicesService;
        private readonly IMessagingService _messagingService;
        private readonly IUsersService _usersService;
        #endregion


        #region ctor

        public OrderFacade(IOrderService orderService, IAppointmentService appointmentService, IStatesService statesService
            , ILaboratoryService laboratoryService, IAppointmentFacade appointmentFacade, ILaboratoryUsersService laboratoryUsersService
            , IUserDevicesService userDevicesService, IMessagingService messagingService, IUsersService usersService)
        {
            _orderService = orderService;
            _appointmentService = appointmentService;
            _statesService = statesService;
            _laboratoryService = laboratoryService;
            _appointmentFacade = appointmentFacade;
            _laboratoryUsersService = laboratoryUsersService;
            _userDevicesService = userDevicesService;
            _messagingService = messagingService;
            _usersService = usersService;
        }
        #endregion

        #region methods
        public PressDotPageListEntityModel<OrderResponse> GetOrdersByAppointmentId(int id, int pageIndex, int pageSize)
        {
            var orderRes = _orderService.GetOrdersByAppointmentId(id, pageIndex, pageSize);

            if (orderRes == null) return null;
            foreach (var item in orderRes)
            {
                item.Laboratory = _laboratoryService.Get(item.LaboratoryId);
                item.Appointment = _appointmentService.Get(item.AppointmentId);
                item.State = _statesService.Get(item.StateId);
            }
            var orders = new PressDotPageListEntityModel<OrderResponse>()
            {
                TotalCount = orderRes.TotalCount,
                Data = orderRes.ToModel<OrderResponse>(),
                HasNextPage = orderRes.HasNextPage,
                HasPreviousPage = orderRes.HasPreviousPage,
                TotalPages = orderRes.TotalPages,
                PageIndex = orderRes.PageIndex,
                PageSize = orderRes.PageSize
            };
            return orders;
        }
        public PressDotPageListEntityModel<OrderResponse> GetOrdersByLaboratoryId(int id, int pageIndex, int pageSize)
        {
            var orderRes = _orderService.GetOrdersByLaboratoryId(id, pageIndex, pageSize);

            if (orderRes == null) return null;
            foreach (var item in orderRes)
            {
                item.Laboratory = _laboratoryService.Get(item.LaboratoryId);
                item.Appointment = _appointmentService.Get(item.AppointmentId);
                item.State = _statesService.Get(item.StateId);
            }
            var orders = new PressDotPageListEntityModel<OrderResponse>()
            {
                TotalCount = orderRes.TotalCount,
                Data = orderRes.ToModel<OrderResponse>(),
                HasNextPage = orderRes.HasNextPage,
                HasPreviousPage = orderRes.HasPreviousPage,
                TotalPages = orderRes.TotalPages,
                PageIndex = orderRes.PageIndex,
                PageSize = orderRes.PageSize
            };
            return orders;
        }

        public bool CreateOrder(CreateOrderRequest request)
        {
            var order = request.ToEntity<Order>();
            var result= _orderService.CreateOrder(order);
            if (result)
            {
                var labUsers = _laboratoryUsersService.GetLaboratoryUsersByLaboratoryId(order.LaboratoryId);
                foreach (var labUser in labUsers)
                {
                    var userDevices = _userDevicesService.GetUserDevicesByUserId(labUser.UserId);
                    var user = _usersService.Get(labUser.UserId);
                    foreach (var userDevice in userDevices)
                    {
                        _messagingService.SendNotification(userDevice.DeviceToken,
                            $"Dear {user.Firstname}",
                            $"New order has been created against appointment no {order.AppointmentId}", userDevice.UserId);
                    }
                }
                
            }
            return result;
        }

        public bool UpdateOrder(UpdateOrderRequest request)
        {
            var order = request.ToEntity<Order>();
            return _orderService.UpdateOrder(order);
        }
        public bool UpdateOrderState(int id, int stateId)
        {
            return _orderService.UpdateOrderState(id, stateId);
        }

        public PressDotPageListEntityModel<OrderResponse> GetOrdersByDoctorId(int doctorId, int pageIndex, int pageSize)
        {
            var orderRes = _orderService.GetOrdersByDoctorId(doctorId, pageIndex, pageSize);

            if (orderRes == null) return null;
            foreach (var item in orderRes)
            {
                item.Laboratory = _laboratoryService.Get(item.LaboratoryId);
                item.Appointment = _appointmentService.Get(item.AppointmentId);
                item.State = _statesService.Get(item.StateId);
            }
            var orders = new PressDotPageListEntityModel<OrderResponse>()
            {
                TotalCount = orderRes.TotalCount,
                Data = orderRes.ToModel<OrderResponse>(),
                HasNextPage = orderRes.HasNextPage,
                HasPreviousPage = orderRes.HasPreviousPage,
                TotalPages = orderRes.TotalPages,
                PageIndex = orderRes.PageIndex,
                PageSize = orderRes.PageSize
            };
            return orders;
        }

        public PressDotPageListEntityModel<OrderResponseForLaboratoryUserId> GetOrdersByLaboratoryUserId(int userId, int stateId, int pageIndex, int pageSize)
        {
            var orderRes = _orderService.GetOrdersByLaboratoryUserId(userId, stateId, pageIndex, pageSize);



            if (orderRes == null) return null;
            var orders = new PressDotPageListEntityModel<OrderResponseForLaboratoryUserId>
            {
                TotalCount = orderRes.TotalCount,
                Data = orderRes.ToModel<OrderResponseForLaboratoryUserId>(),
                HasNextPage = orderRes.HasNextPage,
                HasPreviousPage = orderRes.HasPreviousPage,
                TotalPages = orderRes.TotalPages,
                PageIndex = orderRes.PageIndex,
                PageSize = orderRes.PageSize
            };
            foreach (var item in orders.Data)
            {
                var appointment = _appointmentFacade.GetAppointmentById(item.AppointmentId);
                var state = _statesService.Get(item.StateId);
                if (appointment != null)
                {
                    if (appointment.Doctor != null)
                    {
                        item.DoctorFirstName = appointment.Doctor.Firstname;
                        item.DoctorLastName = appointment.Doctor.Lastname;
                    }
                    if (appointment.Saloon != null)
                    {
                        item.SaloonName = appointment.Saloon.SaloonName;
                    }
                }

                if (state != null)
                {
                    item.State = state.ToModel<StateResponse>();
                }

                item.CreatedDateString = item.CreatedDate.ToString(CultureInfo.InvariantCulture);
            }
            return orders;
        }


        #endregion
    }
}
