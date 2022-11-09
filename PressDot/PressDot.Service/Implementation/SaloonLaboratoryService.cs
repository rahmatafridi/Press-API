using Microsoft.EntityFrameworkCore;
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
    public class SaloonLaboratoryService : BaseService<SaloonLaboratory>, ISaloonLaboratoryService
    {
        #region private

        private readonly ISaloonService _saloonService;

        private readonly ILaboratoryService _laboratoryService;

        #endregion

        #region ctor

        public SaloonLaboratoryService(IRepository<SaloonLaboratory> saloonLaboratoryRepository,
            ISaloonService saloonService, ILaboratoryService laboratoryService) : base(saloonLaboratoryRepository)
        {
            _saloonService = saloonService;
            _laboratoryService = laboratoryService;
        }

        #endregion

        #region methods

        public ICollection<SaloonLaboratory> GetSaloonLaboratoriesBySaloonId(int saloonId)
        {
            return Repository.Table.Include("Laboratory").Where(x => x.SaloonId == saloonId).ToList();
        }

        public ICollection<SaloonLaboratory> GetSaloonLaboratoriesByLaboratoryId(int laboratoryId)
        {
            return Repository.Table.Where(x => x.LaboratoryId == laboratoryId).ToList();
        }

        public SaloonLaboratory ChangeSaloonDefaultLaboratory(SaloonLaboratory entity, bool isUpdate)
        {
            try
            {
                //If record already exists, make it default. else add a default record for it.
                if (isUpdate)
                {
                    UncheckDefaultLabs(saloonId: entity.SaloonId);
                    var dbEntity = Repository.Table.FirstOrDefault(x => x.Id == entity.Id);
                    dbEntity.UpdatedDate = DateTime.Now;
                    dbEntity.IsDefault = entity.IsDefault;
                    Repository.Update(dbEntity);
                    return entity;
                }
                else
                {
                    entity.IsDefault = entity.IsDefault;
                    entity.CreatedDate = DateTime.Now;
                    Repository.Insert(entity);
                    return entity;
                }
            }
            catch (Exception e)
            {
                throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Something_Went_Wrong_While_Updating_SaloonLaboratory,
                                            "Something went wrong while updating saloon laboratory.");
            }


        }

        private bool UncheckDefaultLabs(int saloonId)
        {
            try
            {
                var labs = Repository.Table.Where(x => x.SaloonId == saloonId).ToList();
                if (labs.Count > 0)
                {
                    foreach (var lab in labs)
                    {
                        if (lab.IsDefault)
                        {
                            lab.IsDefault = false;
                            lab.UpdatedDate = DateTime.Now;
                            Repository.Update(lab);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Something_Went_Wrong_While_Updating_SaloonLaboratory
                                            , "Something went wrong while setting default lab to false.");
            }
        }


        public bool DeleteSaloonLaboratory(int id)
        {
            var dbObj = Repository.Table.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (dbObj != null)
            {
                try
                {
                    dbObj.DeletedDate = DateTime.Now;
                    dbObj.IsDeleted = true;
                    Repository.Update(dbObj);
                    return true;
                }
                catch (Exception e)
                {
                    throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Some_Thing_went_wrong_while_deleting_SaloonLaboratory, e.Message);
                }
            }
            else
            {
                throw new PressDotException((int)GeneralExceptionsCodes.Invalid_Id, "Invalid SaloonLaboratoryId.");
            }
        }
        public SaloonLaboratory GetDefaultSaloonLaboratoryBySaloonId(int saloonId)
        {
            var query = Repository.Table;
            query = query.Where(x => x.SaloonId == saloonId && x.IsDeleted == false && x.IsDefault);
            return query.FirstOrDefault();
        }

        public ICollection<SaloonLaboratory> IsLaboratoryAssociatedWithSaloon(int laboratoryId)
        {
            return Repository.Table.Where(x => x.LaboratoryId == laboratoryId).ToList();
        }

        public ICollection<SaloonLaboratory> GetSaloonLaboratoriesBySaloonIds(int[] saloonIds)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId)).ToList();
        }

        public List<SaloonLaboratory> GetSaloonLaboratry(int laboratoryId)
        {
            return Repository.Table.Where(x=>x.LaboratoryId ==laboratoryId).ToList();
        }
        #endregion
    }
}
