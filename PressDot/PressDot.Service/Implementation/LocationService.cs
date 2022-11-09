using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using PressDot.Core.Exceptions;

namespace PressDot.Service.Implementation
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        #region private



        #endregion

        #region cotr

        public LocationService(IRepository<Location> locationRepository) : base(locationRepository)
        {

        }

        #endregion


        #region methods

        public bool CheckLocationExist(string name)
        {
            return Repository.Table.Any(x => x.LocationName.Equals(name));
        }

        public ICollection<Location> GetLocationsByParentId(int locationId)
        {
            return Repository.Table.Where(x => x.ParentLocationId == locationId).ToList();
        }


        public ICollection<Location> GetParentLocations()
        {
            return Repository.Table.Where(x => x.ParentLocationId == null).ToList();
        }

        public int? GetLocationIdByName(string location)
        {
            int? locaitonId = null;
            var dbObj = Repository.Table.Where(x => x.LocationName.Equals(location)).FirstOrDefault();
            if (dbObj != null)
            {
                locaitonId = dbObj.Id;
            }

            return locaitonId;
        }

        public ICollection<Location> GetLocationByName(string locationName)
        {
            return Repository.Table.Where(
                l => l.LocationName.ToLower().Contains(locationName.Trim().ToLower())).ToList();
        }

        public IPagedList<Location> GetLocations(string locationName, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            if (!string.IsNullOrEmpty(locationName))
                query = query.Where(x => x.LocationName.Contains(locationName));

            return new PagedList<Location>(query, pageIndex, pageSize);
        }


        public override void Create(Location entity)
        {
            if (entity.ParentLocationId != null)
            {
                if(CheckLocationExist(entity.LocationName))
                    throw new PressDotException($"City with name of {entity.LocationName} already exist.");
            }
            base.Create(entity);
        }

        #endregion
    }
}
