using AutoMapper;
using PressDot.Contracts.Request.Appointment;
using PressDot.Contracts.Request.Customer;
using PressDot.Contracts.Request.Laboratory;
using PressDot.Contracts.Request.LaboratoryUsers;
using PressDot.Contracts.Request.Location;
using PressDot.Contracts.Request.Order;
using PressDot.Contracts.Request.Saloon;
using PressDot.Contracts.Request.SaloonEmployeeSchedule;
using PressDot.Contracts.Request.SaloonLaboratory;
using PressDot.Contracts.Request.Users;
using PressDot.Contracts.Response.Appointment;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Contracts.Response.Location;
using PressDot.Contracts.Response.Notification;
using PressDot.Contracts.Response.Order;
using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.SaloonEmployeeSchedule;
using PressDot.Contracts.Response.SaloonLaboratory;
using PressDot.Contracts.Response.State;
using PressDot.Contracts.Response.Users;
using PressDot.Core.Domain;
using PressDot.Core.Infrastructure.Mapper;

namespace PressDot.Facade.Framework
{
    public sealed class OrderedMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public int Order => 0;

        #region ctor

        public OrderedMapperConfiguration()
        {
            CreateCustomerMaps();
            CreateUsersRoleMap();
            CreateLocationMap();
            CreateSaloonMap();
            CreateLaboratoryMap();
            CreateSaloonEmployeeScheduleMap();
            CreateStateMap();
            CreateAppointmentMap();
            CreateOrderMap();
            CreateSaloonLaboratory();
            CreateLaboratoryUsers();
            CreateNotificationMaps();
        }
        #endregion

        #region Methods

        private void CreatePagedDataMaps()
        {
            CreateMap<RegisterUsersRequest, Users>();
            CreateMap<Users, UsersRegistrationResponse>();
            CreateMap<Users, UsersResponse>();
        }
        private void CreateNotificationMaps()
        {
            CreateMap<Notification, NotificationResponse>();

        }
        private void CreateCustomerMaps()
        {
            CreateMap<RegisterUsersRequest, Users>();
            CreateMap<Users, UsersRegistrationResponse>();
            CreateMap<Users, UsersResponse>();
            CreateMap<UserUpdateRequest, Users>();
            CreateMap<UserDevices, UserDevicesResponse>();
            CreateMap<UserDeviceRequest, UserDevices>();
        }

        private void CreateUsersRoleMap()
        {
            CreateMap<UserRole, UsersRoleResponse>();
        }


        private void CreateLocationMap()
        {
            CreateMap<LocationCreateRequest, Location>();
            CreateMap<Location, LocationResponse>();
        }

        private void CreateSaloonMap()
        {
            CreateMap<SaloonCreateRequest, Saloon>();
            CreateMap<Saloon, SaloonResponse>();
            CreateMap<SaloonType, SaloonTypeResponse>();
            CreateMap<Saloon, GetSaloonsResponse>();
            CreateMap<SaloonEmployee, SaloonEmployeeResponse>();
            CreateMap<SaloonEmployeeCreateRequest, SaloonEmployee>();
            CreateMap<SaloonUpdateRequest, Saloon>();
            CreateMap<SaloonType, SaloonTypeResponse>();
        }

        private void CreateLaboratoryMap()
        {
            CreateMap<LaboratoryCreateRequest, Laboratory>();
            CreateMap<LaboratoryUpdateRequest, Laboratory>();
            CreateMap<Laboratory, LaboratoryResponse>();
        }
        private void CreateAppointmentMap()
        {
            CreateMap<AppointmentRequest, Appointment>();
            CreateMap<Appointment, AppointmentResponse>()
                .ForMember(x => x.StartTimeString, ad => ad.MapFrom(x => x.StartTime))
                .ForMember(x => x.EndTimeString, ad => ad.MapFrom(x => x.EndTime));
            CreateMap<Appointment, CurrentUserAppointmentResponse>()
                .ForMember(x => x.StartTimeString, ad => ad.MapFrom(x => x.StartTime))
                .ForMember(x => x.EndTimeString, ad => ad.MapFrom(x => x.EndTime));
            
        }

        private void CreateSaloonEmployeeScheduleMap()
        {
            CreateMap<SaloonEmployeeScheduleCreateRequest, SaloonEmployeeSchedule>();
            CreateMap<SaloonEmployeeSchedule, GetSaloonEmployeeScheduleResponse>();
        }
        private void CreateStateMap()
        {
            CreateMap<States, StateResponse>();
        }
        private void CreateOrderMap()
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<Order, OrderResponseForLaboratoryUserId>();
            CreateMap<UpdateOrderRequest, Order>();
            CreateMap<CreateOrderRequest, Order>();
        }

        private void CreateSaloonLaboratory()
        {
            CreateMap<SaloonLaboratory, SaloonLaboratoryResponse>();
            CreateMap<SaloonLaboratoryCreateRequest, SaloonLaboratory>();
        }
        private void CreateLaboratoryUsers()
        {
            CreateMap<CreateLaboratoryUsersRequest, LaboratoryUsers>();
            CreateMap<LaboratoryUsers, LaboratoryUserResponse>();
        }
        #endregion
    }
}
