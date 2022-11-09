using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class LaboratoryService : BaseService<Laboratory>,ILaboratoryService
    {
        #region private

        #endregion

        #region ctor

        public LaboratoryService(IRepository<Laboratory> laboratoryRepository):base(laboratoryRepository)
        {
            
        }

        #endregion

        #region methods

        public IPagedList<Laboratory> GetLaboratories(string name, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.LaboratoryName.Contains(name));

            return new PagedList<Laboratory>(query, pageIndex, pageSize);
        }

        public bool CheckLaboratoryById(int id)
        {
            var lab = Repository.Table.FirstOrDefault(x => x.Id == id);
            return lab != null;
        }

        #endregion
    }
}
