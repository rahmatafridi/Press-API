using System;
using System.Collections.Generic;
using System.Linq;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class LaboratoryUsersService : BaseService<LaboratoryUsers>, ILaboratoryUsersService
    {
        #region private

        #endregion

        #region ctor

        public LaboratoryUsersService(IRepository<LaboratoryUsers> laboratoryUsersRepository) : base(laboratoryUsersRepository)
        {

        }

        #endregion

        #region methods

        public LaboratoryUsers CreateLaboratoryUser(LaboratoryUsers laboratoryUser)
        {
            if (laboratoryUser != null)
            {
                laboratoryUser.CreatedDate = DateTime.UtcNow;
                laboratoryUser.UpdatedDate = DateTime.UtcNow;
                Create(laboratoryUser);
                return laboratoryUser;
            }
            return null;
        }

        public ICollection<LaboratoryUsers> GetLaboratoryUsersByLaboratoryId(int laboratoryId)
        {
            return Repository.Table.Where(x => x.LaboratoryId == laboratoryId).ToList();
        }

        public bool CheckLaboratoryUserExist(int laboratoryId, int userId)
        {
            return Repository.Table.Any(x => x.LaboratoryId == laboratoryId && x.UserId == userId);
        }

        public List<LaboratoryUsers> GetLaboratoryUserById(int userId)
        {
            return Repository.Table.Where(x => x.UserId == userId).ToList();
        }

        #endregion
    }
}
