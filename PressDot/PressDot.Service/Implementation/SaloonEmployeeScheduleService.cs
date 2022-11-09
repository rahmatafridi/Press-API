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
    public class SaloonEmployeeScheduleService : BaseService<SaloonEmployeeSchedule>, ISaloonEmployeeScheduleService
    {
        #region private members


        #endregion

        #region ctor

        public SaloonEmployeeScheduleService(IRepository<SaloonEmployeeSchedule> repository) : base(repository)
        {

        }

        #endregion

        #region methods

        public bool CheckExistenceOfEmployeeScheduledForTheDay(int day, int saloonEmployeeId, TimeSpan startTime, TimeSpan endTime)
        {
            var dbObj = Repository.Table.FirstOrDefault(x => x.SaloonEmployeeId == saloonEmployeeId && x.Day == day
                                                               && ((x.StartTime >= startTime && x.EndTime <= endTime)
                                                                   || (startTime < x.StartTime && endTime >= x.StartTime && endTime <= x.EndTime)
                                                                   || (startTime < x.StartTime && endTime >= x.EndTime)
                                                                   || (startTime> x.StartTime && startTime <= x.EndTime)
                                                                   || (endTime >= x.StartTime && endTime <= x.EndTime))
                                                               );
            return dbObj != null;
        }

        public bool DeleteEmployeeSchedule(int id)
        {
            var dbObj = Repository.Table.FirstOrDefault(x => x.Id == id);
        

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
                    throw new PressDotException((int)SaloonEmployeeExceptionsCodes.Some_Thing_went_wrong_while_deleting_Schedule, e.Message);
                }
            }
            else
            {
                throw new PressDotException((int)GeneralExceptionsCodes.Invalid_Id, "Invalid SaloonEmployeeScheduleId.");
            }
        }

        public bool IsLastScheduleSlot(int saloonEmployeeScheduleId)
        {
            var dbObj = Repository.Table.FirstOrDefault(x => x.Id == saloonEmployeeScheduleId);
            var LastSlot = Repository.Table.FirstOrDefault(x => x.SaloonEmployeeId == dbObj.SaloonEmployeeId
                                                                && x.Id != dbObj.Id && x.IsDeleted == false);

           return LastSlot == null;
        }

        public ICollection<SaloonEmployeeSchedule> GetSaloonEmployeeSchedules_BySaloonEmployeeIdAndDayId(
            int saloonEmployeeId, int dayId)
        {
            var dbData = Repository.Table.Where(x => x.SaloonEmployeeId == saloonEmployeeId && x.Day == dayId).ToList();
            return dbData;
        }

        public ICollection<SaloonEmployeeSchedule> GetSaloonEmployeeSchedulesBySaloonEmployeeIds(
            int[] saloonEmployeeId)
        {
            return Repository.Table.Where(x => saloonEmployeeId.Contains(x.SaloonEmployeeId)).ToList();
            
        }

        #endregion
    }
}
