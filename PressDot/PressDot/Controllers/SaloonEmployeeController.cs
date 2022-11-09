using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Saloon;
using PressDot.Contracts.Request.SaloonEmployeeSchedule;
using PressDot.Contracts.Response.Saloon;
using PressDot.Core.Domain;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/SaloonEmployee")]
    public class SaloonEmployeeController : AuthenticatedController
    {
        #region private

        private readonly ISaloonEmployeeFacade _saloonEmployeeFacade;

        private readonly ISaloonEmployeeService _saloonEmployeeService;

        private readonly ISaloonEmployeeScheduleService _saloonEmployeeScheduleService;

        #endregion

        #region ctor

        public SaloonEmployeeController(ISaloonEmployeeFacade saloonEmployeeFacade, ISaloonEmployeeService saloonEmployeeService,
            ISaloonEmployeeScheduleService saloonEmployeeScheduleService)
        {
            _saloonEmployeeFacade = saloonEmployeeFacade;
            _saloonEmployeeService = saloonEmployeeService;
            _saloonEmployeeScheduleService = saloonEmployeeScheduleService;
        }

        #endregion

        #region actions

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(SaloonEmployeeCreateRequest saloonEmployeeCreateRequest)
        {
            var saloonEmployee = saloonEmployeeCreateRequest.ToEntity<SaloonEmployee>();
            _saloonEmployeeService.Create(saloonEmployee);
            return Ok(saloonEmployee.ToModel<SaloonEmployeeResponse>());
        }

        [HttpGet]
        [Route("GetSaloonEmployeesByEmployeeId/{employeeId}")]
        public ActionResult GetSaloonEmployeesByEmployeeId(int employeeId)
        {
            return Ok(_saloonEmployeeFacade.GetSaloonEmployeesByEmployeeId(employeeId));
        }

        [HttpGet]
        [Route("GetSaloonEmployeesBySaloonId/{saloonId}")]
        public ActionResult GetSaloonEmployeesBySaloonId(int saloonId)
        {
            return Ok(_saloonEmployeeFacade.GetSaloonEmployeesBySaloonId(saloonId));
        }


        [HttpPost]
        [Route("ScheduleSaloonEmployee")]
        public ActionResult ScheduleSaloonEmployee(SaloonEmployeeScheduleCreateRequest saloonEmployees)
        {

            var result = _saloonEmployeeFacade.AttachEmployeeWithSaloonForDaysOfWeek(saloonEmployees);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSaloonEmployeeSchedule")]
        public ActionResult DeleteSaloonEmployeeSchedule(int saloonEmployeeScheduleId)
        {
            var result = _saloonEmployeeFacade.DeleteEmployeeSchedule(saloonEmployeeScheduleId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSaloonEmployee")]
        public ActionResult DeleteSaloonEmployee(int saloonEmployeeId)
        {
            var result = _saloonEmployeeFacade.DeleteSaloonEmployee(saloonEmployeeId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSaloonAdministratorsBySaloonId/{saloonId}")]
        public ActionResult GetSaloonAdministratorsBySaloonId(int saloonId)
        {
            return Ok(_saloonEmployeeFacade.GetSaloonAdministratorsBySaloonId(saloonId));
        }
        #endregion
    }
}
