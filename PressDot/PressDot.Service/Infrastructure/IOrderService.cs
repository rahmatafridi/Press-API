using PressDot.Core;
using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface IOrderService : IService<Order>
    {
        IPagedList<Order> GetOrdersByAppointmentId(int appointmentId, int pageIndex, int pageSize);
        IPagedList<Order> GetOrdersByLaboratoryId(int laboratoryId, int pageIndex, int pageSize);
        bool IsAnyPendingOrdersByLaboratoryAndSaloon(int laboratoryId, int saloonId);
        bool IsAnyPendingOrdersByLaboratory(int laboratoryId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool UpdateOrderState(int id, int stateId);
        IPagedList<Order> GetOrdersByDoctorId(int doctorId, int pageIndex, int pageSize);
        IPagedList<Order> GetOrdersByLaboratoryUserId(int userId, int stateId, int pageIndex, int pageSize);
    }
}
