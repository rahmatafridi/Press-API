using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PressDot.Contracts.Request.Appointment;
using PressDot.Facade.Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v2;
using WooCommerceNET.WooCommerce.v2.Extension;

namespace PressDot.Controllers
{
    [Route("api/v1/Appointment")]
    public class AppointmentController : AuthenticatedController
    {
        #region private

        private readonly IAppointmentFacade _appointmentFacade;

        #endregion

        #region ctor

        public AppointmentController(IAppointmentFacade appointmentFacade)
        {
            _appointmentFacade = appointmentFacade;
        }

        #endregion

        #region actions


        [HttpGet]
        [Route("GetAppointmentbySaloonId/")]
        public ActionResult GetAppointmentbySaloonId(int saloonId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbySaloonId(saloonId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentbyDoctorId/")]
        public ActionResult GetAppointmentbyDoctorId(int doctorId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbyDoctorId(doctorId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentApprovebyDoctorId/")]
        public ActionResult GetAppointmentApprovebyDoctorId(int doctorId,int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentApprovedbyDoctorId(doctorId,statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentActivebyDoctorId/")]
        public ActionResult GetAppointmentActivebyDoctorId(int doctorId, int statusId,int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentActivebyDoctorId(doctorId,statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentRejectbyDoctorId/")]
        public ActionResult GetAppointmentRejectbyDoctorId(int doctorId, int statusId,int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentRejectbyDoctorId(doctorId,statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentPendingtbyDoctorId/")]
        public ActionResult GetAppointmentPendingbyDoctorId(int doctorId, int statusId,int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentPendingbyDoctorId(doctorId,statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentCancelbyDoctorId/")]
        public ActionResult GetAppointmentCancelbyDoctorId(int doctorId, int statusId,int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentCancelbyDoctorId(doctorId,statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentbyCustomerId/")]
        public ActionResult GetAppointmentbyCustomerId(int customerId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbyCustomerId(customerId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointmentbySaloonAdministratorId/")]
        public ActionResult GetAppointmentbySaloonAdministratorId(int saloonAdministratorId,int stateId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbySaloonAdministratorId(saloonAdministratorId,stateId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpPost]
        [Route("GetSaloonAppointmentsForSaloonAdministrator/")]
        public ActionResult GetSaloonAppointmentsForSaloonAdministrator([FromBody]AppointmentStateRequest states,int userId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetSaloonAppointmentsForSaloonAdministrator(states,userId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointmentsbyAppointmentStates/")]
        public ActionResult GetAppointmentsbyAppointmentStates(AppointmentStateRequest states, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentsbyAppointmentStates(states, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetCurrentUserAppointments/")]
        public ActionResult GetCurrentUserAppointments(int customerId, bool isFutureAppointments = true,
            int pageIndex = 0, int pageSize = 20)

        {
            var appointments =
                _appointmentFacade.GetCurrentUserAppointments(customerId, isFutureAppointments, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpPut]
        [Route("UpdateAppointment/")]
        public ActionResult UpdateAppointment(AppointmentRequest request)
        {
            var result = _appointmentFacade.UpdateAppointment(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateAppointmentState/")]
        public ActionResult UpdateAppointmentState(int id, int stateId)
        {
            var result = _appointmentFacade.UpdateAppointmentState(id, stateId);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateAppointment/")]
        public ActionResult CreateAppointment(AppointmentRequest request)
        {
            var result = _appointmentFacade.CreateAppointment(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("LoadpreviousAppintment/")]
        public ActionResult LoadpreviousAppintment(int userId,int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetCompleteAppointmentForDashboard(userId, statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("LoadApprovedAppintment/")]
        public ActionResult LoadApprovedAppintment(int userId, int statusId,string search, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetApprovedAppointmentForDashboard(userId, statusId,search, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("LoadPendingAppintment/")]
        public ActionResult LoadPendingAppintment(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetPendingAppointmentForDashboard(userId, statusId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("LoadRejectAppintment/")]
        public ActionResult LoadRejectAppintment(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetRejectAppointmentForDashboard(userId, statusId, pageIndex, pageSize);
            return Ok(appointments);
        } [HttpGet]
        [Route("LoadCancelAppintment/")]
        public ActionResult LoadCancelAppintment(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetCancelAppointmentForDashboard(userId, statusId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("LoadAppointmentApproveForLab")]
        public ActionResult LoadAppointmentForLab(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var products = _appointmentFacade.GetApprovedAppointmentForLab(userId,statusId,pageIndex,pageSize);
            return Ok(products);
        }
        [HttpGet]
        [Route("LoadAppointmentPendingForLab")]
        public ActionResult LoadAppointmentPendingForLab(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var products = _appointmentFacade.GetPendingAppointmentForLab(userId,statusId,pageIndex,pageSize);
            return Ok(products);
        }
        [HttpGet]
        [Route("LoadAppointmentRejectForLab")]
        public ActionResult LoadAppointmentRejectForLab(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var products = _appointmentFacade.GetRejectAppointmentForLab(userId,statusId,pageIndex,pageSize);
            return Ok(products);
        }

        [HttpGet]
        [Route("LoadAppointmentCancelForLab")]
        public ActionResult LoadAppointmentCancelForLab(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var products = _appointmentFacade.GetCancelAppointmentForLab(userId, statusId, pageIndex, pageSize);
            return Ok(products);
        }
        [HttpGet]
        [Route("LoadAppointmentForLabApprove")]
        public ActionResult LoadAppointmentForLabApprove(int userId, int statusId, int pageIndex = 0, int pageSize = 20)
        {
            var products = _appointmentFacade.GetActiveAppointmentForLabApprove(userId, statusId, pageIndex, pageSize);
            return Ok(products);
        }
        
        #endregion
    }

}
