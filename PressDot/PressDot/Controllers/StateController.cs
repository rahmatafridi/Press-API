using Microsoft.AspNetCore.Mvc;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/State")]
    public class StateController : AuthenticatedController
    {
        #region private

        private readonly IStatesService _statesService;

        #endregion

        #region ctor

        public StateController(IStatesService statesService)
        {
            _statesService = statesService;
        }

        #endregion

        #region actions


        [HttpGet]
        [Route("GetAppointmentStates/")]
        public ActionResult GetAppointmentStates()
        {
            var appointments = _statesService.GetAppointmentStates();
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetOrderStates/")]
        public ActionResult GetOrderStates()
        {
            var appointments = _statesService.GetOrderStates();
            return Ok(appointments);
        }

        #endregion
    }

}
