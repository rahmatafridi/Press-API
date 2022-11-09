using System;
using System.Collections.Generic;
using System.Text;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class SaloonTypeService : BaseService<SaloonType>,ISaloonTypeService
    {
        #region private



        #endregion


        #region ctor

        public SaloonTypeService(IRepository<SaloonType> saloonTypeRepository):base(saloonTypeRepository)
        {
            
        }

        #endregion

        #region methods

        #endregion
    }
}
