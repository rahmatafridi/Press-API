using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Laboratory;
using PressDot.Contracts.Request.LaboratoryUsers;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Contracts.Response.Saloon;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/LaboratoryUsers")]
    public class LaboratoryUsersController : AuthenticatedController
    {
        #region private

        private readonly ILaboratoryUsersFacade _laboratoryUsersFacade;

        #endregion

        #region ctor

        public LaboratoryUsersController(ILaboratoryUsersFacade laboratoryUsersFacade)
        {
            _laboratoryUsersFacade = laboratoryUsersFacade;
        }
        #endregion

        #region actions
        [HttpPost]
        [Route("CreateLaboratoryUser/")]
        public ActionResult CreateLaboratoryUser(CreateLaboratoryUsersRequest request)
        {
            var result = _laboratoryUsersFacade.CreateLaboratoryUser(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLaboratoryUsersByLaboratoryId/{laboratoryId}")]
        public ActionResult GetLaboratoryUsersByLaboratoryId(int laboratoryId)
        {
            var result = _laboratoryUsersFacade.GetLaboratoryUsersByLaboratoryId(laboratoryId);
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteLaboratoryUser")]
        public ActionResult DeleteLaboratoryUser(int laboratoryUserId)
        {
            var result = _laboratoryUsersFacade.DeleteLaboratoryUser(laboratoryUserId);
            return Ok(result);
        }
        #endregion
    }
}
