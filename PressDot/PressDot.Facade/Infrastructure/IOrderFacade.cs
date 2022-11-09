using PressDot.Contracts;
using PressDot.Contracts.Request.Order;
using PressDot.Contracts.Response.Order;

namespace PressDot.Facade.Infrastructure
{
    public interface IOrderFacade
    {
        PressDotPageListEntityModel<OrderResponse> GetOrdersByAppointmentId(int id, int pageIndex, int pageSize);
        PressDotPageListEntityModel<OrderResponse> GetOrdersByLaboratoryId(int id, int pageIndex, int pageSize);

        PressDotPageListEntityModel<OrderResponseForLaboratoryUserId> GetOrdersByLaboratoryUserId(int userId,
            int stateId, int pageIndex, int pageSize);
        bool CreateOrder(CreateOrderRequest request);
        bool UpdateOrder(UpdateOrderRequest request);
        bool UpdateOrderState(int id, int stateId);
        PressDotPageListEntityModel<OrderResponse> GetOrdersByDoctorId(int doctorId, int pageIndex, int pageSize);
    }
}
