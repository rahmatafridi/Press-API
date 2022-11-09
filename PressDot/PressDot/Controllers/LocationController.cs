using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts;
using PressDot.Contracts.Request.Location;
using PressDot.Contracts.Response.Location;
using PressDot.Core.Domain;
using PressDot.Facade.Framework.Extensions;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Location")]
    public class LocationController : AuthenticatedController
    {
        #region private

        private readonly ILocationService _locationService;

        #endregion

        #region ctor

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        #endregion

        #region Action

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(LocationCreateRequest locationCreateRequest)
        {
            var location = locationCreateRequest.ToEntity<Location>();
            _locationService.Create(location);
            return Ok(location.ToModel<LocationResponse>());
        }


        [HttpGet]
        [Route("GetLocationsByParentId/{locationId}")]
        public ActionResult GetLocationsByParentId(int locationId)
        {
            var locations = _locationService.GetLocationsByParentId(locationId);
            if (locations == null)
                return Ok();
            return Ok(locations.ToModel<LocationResponse>());
        }


        [HttpGet]
        [Route("GetParentLocations")]
        public ActionResult GetParentLocations()
        {
            var locations = _locationService.GetParentLocations();
            if (locations == null)
                return Ok();
            return Ok(locations.ToModel<LocationResponse>());
        }

        [HttpGet]
        [Route("GetLocationByName")]
        public ActionResult GetLocations(string name)
        {
            var locations = _locationService.GetLocationByName(name);
            if (locations == null)
                return Ok();
            return Ok(locations.ToModel<LocationResponse>());
        }

        [HttpGet]
        [Route("GetLocationIdByName")]
        public ActionResult GetLocationIdByName(string name)
        {
            var locationId = _locationService.GetLocationIdByName(name);
            if (locationId == null)
                return Ok();
            return Ok(locationId);
        }

        [HttpGet]
        [Route("GetLocations/")]
        public ActionResult GetLocations(string name, int pageIndex = 0, int pageSize = 10)
        {
            var locations = _locationService.GetLocations(name,pageIndex,pageSize);
            if (locations == null)
                return Ok();

            var locationPagedList = new PressDotPageListEntityModel<LocationResponse>()
            {
                Data = locations.ToModel<LocationResponse>(),
                HasNextPage = locations.HasNextPage,
                HasPreviousPage = locations.HasPreviousPage,
                PageIndex = locations.PageIndex,
                PageSize = locations.PageSize,
                TotalCount = locations.TotalCount,
                TotalPages = locations.TotalPages
            };

            return Ok(locationPagedList);
        }
        #endregion
    }
}
