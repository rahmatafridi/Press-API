using Microsoft.EntityFrameworkCore;
using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class SaloonService : BaseService<Saloon>, ISaloonService
    {
        #region private members


        #endregion

        #region ctor

        public SaloonService(IRepository<Saloon> repository) : base(repository)
        {

        }

        #endregion

        #region methods

        public IPagedList<Saloon> GetSaloons(string name, int? countryId, int? cityId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.SaloonName.Contains(name));
            if (countryId != null)
                query = query.Where(x => x.CountryId == countryId.Value);
            if (cityId != null)
                query = query.Where(x => x.CityId == cityId.Value);

            return new PagedList<Saloon>(query.OrderByDescending(x => x.CreatedDate), pageIndex, pageSize);
        }

        public List<Saloon> GetSaloonsByLocationId(int locId)
        {
            List<Saloon> saloons = new List<Saloon>();
            var dbData = Repository.Table.Where(x => x.CountryId == locId || x.CityId == locId);
            if (dbData.Any())
            {
                saloons = dbData.ToList();
            }

            return saloons;
        }

        public bool CheckSaloonById(int id)
        {
            var saloon = Repository.Table.FirstOrDefault(x => x.Id == id);
            return saloon != null;
        }

        public Saloon UpdateSaloon(Saloon saloon)
        {
            try
            {
                var dbObj = Repository.Table.FirstOrDefault(x => x.Id == saloon.Id);
                if (dbObj != null)
                {
                    dbObj.CountryId = saloon.CountryId;
                    dbObj.CityId = saloon.CityId;
                    dbObj.SaloonName = saloon.SaloonName;
                    dbObj.Email = saloon.Email;
                    dbObj.Address = saloon.Address;
                    dbObj.Phone = saloon.Phone;
                    Repository.Update(dbObj);
                    return saloon;
                }
                else
                {
                    throw new PressDotException("Saloon not found.");
                }
            }
            catch (Exception e)
            {
                throw new PressDotException(e.Message);
            }
        }

        public Saloon GetSaloonById(int id)
        {
            var saloon = Repository.Table.Include("SaloonType").FirstOrDefault(x => x.Id == id);
            return saloon;
        }

        public bool DeleteSaloon(Saloon entity)
        {

            try
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                Repository.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                throw new PressDotException((int)SaloonExceptionsCodes.Something_Went_Wrong_While_Deleting_Saloon, "Something went wrong while deleting saloon. Message: " + e.Message);
            };
        }

        public ICollection<Saloon> GetSaloons(string searchTerm)
        {
            return Repository.Table.Where(x => x.SaloonName.Contains(searchTerm)).ToList();
        }

        #endregion
    }
}
