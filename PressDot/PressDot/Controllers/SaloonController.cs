using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Saloon;
using PressDot.Contracts.Request.SaloonLaboratory;
using PressDot.Contracts.Response.Saloon;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;

namespace PressDot.Controllers
{
    [Route("api/v1/Saloon")]
    public class SaloonController : AuthenticatedController
    {
        #region private

        private readonly ISaloonService _saloonService;

        private readonly ISaloonFacade _saloonFacade;

        private readonly ISaloonLaboratoryService _saloonLaboratoryService;

        #endregion


        #region ctor

        public SaloonController(ISaloonService saloonService, ISaloonFacade saloonFacade, ISaloonLaboratoryService saloonLaboratoryService)
        {
            _saloonService = saloonService;
            _saloonFacade = saloonFacade;
            _saloonLaboratoryService = saloonLaboratoryService;
        }

        #endregion

        #region actions

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(SaloonCreateRequest saloonCreateRequest)
        {
            var saloon = saloonCreateRequest.ToEntity<Saloon>();
            _saloonService.Create(saloon);
            return Ok(saloon.ToModel<SaloonResponse>());
        }


        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult Get(int id)
        {
            var saloon = _saloonFacade.GetSaloonById(id);
            if (saloon == null)
                throw new PressDotNotFoundException(1021, $"Saloon not found for given id {id}");
            return Ok(saloon.ToModel<SaloonResponse>());
        }


        [HttpGet]
        [Route("GetSaloons/")]
        public ActionResult GetSaloons(string name, int? countryId, int? cityId, int pageIndex = 0, int pageSize = 10)
        {
            return Ok(_saloonFacade.GetSaloons(name, countryId, cityId, pageIndex, pageSize));
        }

        [HttpGet]
        [Route("GetSaloonList/")]
        public ActionResult GetSaloons(string name)
        {
            return Ok(_saloonFacade.GetSaloon(name).ToModel<SaloonResponse>());
        }

        [HttpGet]
        [Route("GetSaloonsByLocation/{locationName}")]
        public ActionResult GetSaloonsByLocation(string locationName)
        {
            return Ok(_saloonFacade.GetSaloonsByLocation(locationName));
        }

        [HttpGet]
        [Route("GetSaloonsByLocationOrSaloonName/")]
        public ActionResult GetSaloonsByLocationOrSaloonName(string locationName,string saloonName)
        {
            return Ok(_saloonFacade.GetSaloonsByLocationOrSaloonName(locationName,saloonName));
        }

        /// <summary>
        /// Get saloons by location id
        /// </summary>
        /// <param name="locationId">Location Id</param>
        /// <returns>List of Saloons</returns>
        [HttpGet]
        [Route("GetSaloonsByLocationId/{locationId}")]
        public ActionResult GetSaloonsByLocationId(int locationId)
        {
            return Ok(_saloonFacade.GetSaloonsByLocationId(locationId));
        }

        [HttpGet]
        [Route("GetSaloonAppointments/")]
        public ActionResult GetSaloonAppointments(int saloonId, DateTime? date)
        {
            return Ok(_saloonFacade.GetSaloonAppointments(saloonId, date));
        }

        [HttpPost]
        [Route("UpdateSaloon")]
        public ActionResult UpdateSaloon(SaloonUpdateRequest saloonUpdateRequest)
        {
            var result = _saloonFacade.UpdateSaloon(saloonUpdateRequest);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSaloon")]
        public ActionResult DeleteSaloon(int saloonId)
        {
            var result = _saloonFacade.DeleteSaloon(saloonId);
            return Ok(result);
        }

        [HttpPost]
        [Route("ChangeSaloonDefaultLaboratory")]
        public IActionResult ChangeSaloonDefaultLaboratory([FromBody] SaloonLaboratoryCreateRequest request)
        {
            var resutl = _saloonFacade.ChangeDefaultLaboratory(request);
            return Ok(resutl);
        }

        [HttpDelete]
        [Route("DeleteSaloonLaboratory")]
        public ActionResult DeleteSaloonLaboratory(int saloonLaboratoryId)
        {
            var result = _saloonFacade.DeleteSaloonLaboratory(saloonLaboratoryId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLaboratoriesBySaloonId/{saloonId}")]
        public ActionResult GetLaboratoriesBySaloonId(int saloonId)
        {
            return Ok(_saloonFacade.GetLaboratoriesBySaloonId(saloonId));
        }


        #endregion
    }
}
