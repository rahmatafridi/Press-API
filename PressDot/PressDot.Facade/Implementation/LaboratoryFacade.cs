using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PressDot.Contracts;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Facade.Implementation
{
    public class LaboratoryFacade : ILaboratoryFacade
    {
        #region private

        private readonly ILaboratoryService _laboratoryService;

        private readonly ISaloonLaboratoryService _saloonLaboratoryService;

        private readonly ISaloonService _saloonService;

        #endregion

        #region ctor

        public LaboratoryFacade(ILaboratoryService laboratoryService,
            ISaloonLaboratoryService saloonLaboratoryService,
            ISaloonService saloonService)
        {
            _laboratoryService = laboratoryService;
            _saloonLaboratoryService = saloonLaboratoryService;
            _saloonService = saloonService;
        }

        #endregion

        #region methods

        public PressDotPageListEntityModel<LaboratoryResponse> GetLaboratories(string name, int pageIndex, int pageSize)
        {
            var laboratoryList = _laboratoryService.GetLaboratories(name, pageIndex, pageSize);
            var laboratories = new PressDotPageListEntityModel<LaboratoryResponse>()
            {
                TotalCount = laboratoryList.TotalCount,
                Data = laboratoryList.ToModel<LaboratoryResponse>(),
                HasNextPage = laboratoryList.HasNextPage,
                HasPreviousPage = laboratoryList.HasPreviousPage,
                TotalPages = laboratoryList.TotalPages,
                PageIndex = laboratoryList.PageIndex,
                PageSize = laboratoryList.PageSize

            };
            return laboratories;
        }

        public bool DeleteLaboratory(int laboratoryId)
        {
            var laboratory = _laboratoryService.Get(laboratoryId);
            if(laboratory == null)
                throw new PressDotException("No laboratory exist to delete.");
            var deleteLab = true;

            // Logic to check if saloon is associated with Lab and saloon is not deleted.
            var saloonLaboratories = _saloonLaboratoryService.IsLaboratoryAssociatedWithSaloon(laboratoryId);
            if (saloonLaboratories.Count > 0)
            {
                var saloons = _saloonService.Get(saloonLaboratories.Select(x => x.SaloonId).ToArray());
                if (saloons.Count > 0)
                    deleteLab = false; 
            }

            if (deleteLab)
            {
                _laboratoryService.Remove(laboratory);
                return true;
            }
            else
            {
                throw new PressDotException("Laboratory is associated with saloons. Please remove association with saloons.");
            }
        }

        public bool UpdateLaboratory(Laboratory laboratory)
        {
            var laboratoryEdit = _laboratoryService.Get(laboratory.Id);
            if(laboratoryEdit == null)
                throw new PressDotException("Laboratory does not exist or deleted from system.");
            laboratoryEdit.LaboratoryName = laboratory.LaboratoryName;
            return _laboratoryService.Update(laboratoryEdit);
        }
        #endregion
    }
}
