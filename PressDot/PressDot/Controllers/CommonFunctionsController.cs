using Microsoft.AspNetCore.Mvc;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/CommonFunctions")]
    public class CommonFunctionsController : AuthenticatedController
    {
        #region private

        private readonly IDaysOfWeekService _daysOfWeek;

        #endregion


        #region ctor

        public CommonFunctionsController(IDaysOfWeekService daysOfWeek)
        {
            _daysOfWeek = daysOfWeek;
        }

        #endregion

        #region actions


        [HttpGet]
        [Route("GetDays")]
        public ActionResult GetDays()
        {
            return Ok(_daysOfWeek.GetDays());
        }
        #endregion
    }
}