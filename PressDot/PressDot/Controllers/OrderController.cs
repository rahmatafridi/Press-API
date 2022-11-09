using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Order;
using PressDot.Facade.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Order")]
    public class OrderController : AuthenticatedController
    {
        #region private

        private readonly IOrderFacade _orderFacade;

        #endregion

        #region ctor

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        #endregion

        #region actions

        [HttpGet]
        [Route("GetOrdersByAppointmentId/")]
        public ActionResult GetOrdersByAppointmentId(int appointmentId, int pageIndex = 0, int pageSize = 20)
        {
            var orders = _orderFacade.GetOrdersByAppointmentId(appointmentId, pageIndex, pageSize);
            return Ok(orders);
        }
        [HttpGet]
        [Route("GetOrdersByLaboratoryId/")]
        public ActionResult GetOrdersByLaboratoryId(int laboratoryId, int pageIndex = 0, int pageSize = 20)
        {
            var orders = _orderFacade.GetOrdersByLaboratoryId(laboratoryId, pageIndex, pageSize);
            return Ok(orders);
        }
        [HttpGet]
        [Route("GetOrdersByDoctorId/")]
        public ActionResult GetOrdersByDoctorId(int doctorId, int pageIndex = 0, int pageSize = 20)
        {
            var orders = _orderFacade.GetOrdersByDoctorId(doctorId, pageIndex, pageSize);
            return Ok(orders);
        }
        [HttpGet]
        [Route("GetOrdersByLaboratoryUserId/")]
        public ActionResult GetOrdersByLaboratoryUserId(int userId, int stateId = 0, int pageIndex = 0, int pageSize = 20)
        {
            var orders = _orderFacade.GetOrdersByLaboratoryUserId(userId, stateId, pageIndex, pageSize);
            return Ok(orders);
        }

        [HttpPost]
        [Route("CreateOrder/")]
        public ActionResult CreateOrder(CreateOrderRequest request)
        {
            var result = _orderFacade.CreateOrder(request);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateOrder/")]
        public ActionResult UpdateOrder(UpdateOrderRequest request)
        {
            var result = _orderFacade.UpdateOrder(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateOrderState/")]
        public ActionResult UpdateOrderState(int id, int stateId)
        {
            var result = _orderFacade.UpdateOrderState(id, stateId);
            return Ok(result);
        }

        #endregion
    }

}
