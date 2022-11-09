using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Laboratory;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Contracts.Response.Saloon;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Laboratory")]
    public class LaboratoryController : AuthenticatedController
    {
        #region private

        private readonly ILaboratoryService _laboratoryService;

        private readonly ILaboratoryFacade _laboratoryFacade;

        #endregion

        #region ctor

        public LaboratoryController(ILaboratoryService laboratoryService, ILaboratoryFacade laboratoryFacade)
        {
            _laboratoryService = laboratoryService;
            _laboratoryFacade = laboratoryFacade;
        }
        #endregion

        #region actions

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(LaboratoryCreateRequest laboratoryCreateRequest)
        {
            var laboratory = laboratoryCreateRequest.ToEntity<Laboratory>();
            _laboratoryService.Create(laboratory);
            return Ok(laboratory.ToModel<LaboratoryResponse>());
        }

        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult Get(int id)
        {
            var laboratory = _laboratoryService.Get(id);
            if (laboratory == null)
                throw new PressDotNotFoundException(1041, $"Laboratory not found for given id {id}");
            return Ok(laboratory.ToModel<LaboratoryResponse>());
        }


        [HttpGet]
        [Route("GetLaboratories/")]
        public ActionResult GetLaboratories(string name, int pageIndex = 0, int pageSize = 10)
        {
            return Ok(_laboratoryFacade.GetLaboratories(name,  pageIndex, pageSize));
        }

        [HttpGet]
        [Route("GetAllLaboratories/")]
        public ActionResult GetAllLaboratories()
        {
            return Ok(_laboratoryService.Get().ToModel<LaboratoryResponse>());
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int laboratoryId)
        {
            return Ok(_laboratoryFacade.DeleteLaboratory(laboratoryId));
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(LaboratoryUpdateRequest laboratoryUpdateRequest)
        {
            var laboratory = laboratoryUpdateRequest.ToEntity<Laboratory>();
            return Ok(_laboratoryFacade.UpdateLaboratory(laboratory));
        }
        #endregion
    }
}
