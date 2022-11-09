using System;
using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        #region private members
        private readonly IAppointmentService _appointmentService;
        private readonly IStatesService _stateService;
        private readonly ISaloonLaboratoryService _saloonLaboratoryService;
        private readonly ILaboratoryUsersService _laboratoryUsersService;

        #endregion

        #region ctor

        public OrderService(IRepository<Order> repository, IAppointmentService appointmentService, IStatesService stateService, ISaloonLaboratoryService saloonLaboratoryService, ILaboratoryUsersService laboratoryUsersService) : base(repository)
        {
            _appointmentService = appointmentService;
            _stateService = stateService;
            _saloonLaboratoryService = saloonLaboratoryService;
            _laboratoryUsersService = laboratoryUsersService;
        }

        #endregion

        #region methods
        public IPagedList<Order> GetOrdersByAppointmentId(int appointmentId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(o => o.AppointmentId == appointmentId);
            return new PagedList<Order>(query, pageIndex, pageSize);
        }
        public IPagedList<Order> GetOrdersByLaboratoryId(int laboratoryId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(o => o.LaboratoryId == laboratoryId);
            return new PagedList<Order>(query, pageIndex, pageSize);
        }
        public IPagedList<Order> GetOrdersByDoctorId(int doctorId, int pageIndex, int pageSize)
        {
            var appointments = _appointmentService.GetAppointmentsbyDoctorId(doctorId).Select(ap => ap.Id).ToList();
            var query = Repository.Table;
            query = query.Where(o => appointments.Contains(o.AppointmentId) && !o.IsDeleted);
            return new PagedList<Order>(query, pageIndex, pageSize);
        }
        public bool IsAnyPendingOrdersByLaboratoryAndSaloon(int laboratoryId, int saloonId)
        {
            var appointment = _appointmentService.GetAppointmentsBySaloonId(saloonId).Select(ap => ap.Id).ToList();
            var states = _stateService.GetStates().Where(s => s.StateFor.Equals("Order") && !s.Value.Equals("Complete") && !s.IsDeleted).Select(s => s.Id);
            var query = Repository.Table;
            query = query.Where(o => o.LaboratoryId == laboratoryId && appointment.Contains(o.AppointmentId) && states.Contains(o.StateId));
            if (query.Any())
                return true;
            return false;
        }
        public bool IsAnyPendingOrdersByLaboratory(int laboratoryId)
        {
            var states = _stateService.GetStates().Where(s => s.StateFor.Equals("Order") && !s.Value.Equals("Complete") && !s.IsDeleted).Select(s => s.Id);
            var query = Repository.Table;
            query = query.Where(o => o.LaboratoryId == laboratoryId && states.Contains(o.StateId));
            if (query.Any())
                return true;
            return false;
        }
        public bool CreateOrder(Order order)
        {
            var appointment = _appointmentService.Get(order.AppointmentId);
            if (appointment != null)
            {
                var saloonDefaultLaboratory = _saloonLaboratoryService.GetDefaultSaloonLaboratoryBySaloonId(appointment.SaloonId);
                if (saloonDefaultLaboratory != null)
                {
                    order.StateId = _stateService.GetStatesByStateNameAndType("Created", "Order").Id;
                    order.LaboratoryId = saloonDefaultLaboratory.LaboratoryId;
                    order.CreatedDate = DateTime.UtcNow;
                    order.UpdatedDate = DateTime.UtcNow;
                    Create(order);
                    return true;
                }
            }

            return false;
        }
        public bool UpdateOrder(Order order)
        {
            var dbOrderObj = Get(order.Id);
            if (dbOrderObj != null)
            {
                dbOrderObj.AppointmentId = order.AppointmentId;
                dbOrderObj.LaboratoryId = order.LaboratoryId;
                dbOrderObj.StateId = order.StateId;
                dbOrderObj.Description = order.Description;
                dbOrderObj.UpdatedBy = order.UpdatedBy;
                dbOrderObj.UpdatedDate = DateTime.UtcNow;
                return Update(dbOrderObj);
            }
            return false;
        }

        public bool UpdateOrderState(int id, int stateId)
        {
            var dborderObj = Get(id);
            if (dborderObj != null)
            {
                dborderObj.StateId = stateId;
                dborderObj.UpdatedDate = DateTime.UtcNow;
                return Update(dborderObj);
            }
            return false;
        }
        public IPagedList<Order> GetOrdersByLaboratoryUserId(int userId, int stateId, int pageIndex, int pageSize)
        {
            var laboratory = _laboratoryUsersService.Get().FirstOrDefault(lus => lus.UserId == userId);
            if (laboratory == null) return null;
            var query = Repository.Table;
            if (stateId == 0)
            {
                var createdStateId = _stateService.GetStatesByStateNameAndType("Created", "Order").Id;
                query = query.Where(o => o.LaboratoryId == laboratory.LaboratoryId && o.StateId != createdStateId && !o.IsDeleted);
            }
            else
            {
                query = query.Where(o => o.LaboratoryId == laboratory.LaboratoryId && o.StateId == stateId && !o.IsDeleted);
            }

            return new PagedList<Order>(query, pageIndex, pageSize);
        }

        #endregion
    }
}
