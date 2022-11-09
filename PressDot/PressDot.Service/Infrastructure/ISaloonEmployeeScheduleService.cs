using System;
using System.Collections.Generic;
using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface ISaloonEmployeeScheduleService : IService<SaloonEmployeeSchedule>
    {
        bool CheckExistenceOfEmployeeScheduledForTheDay(int day, int saloonEmployeeId, TimeSpan startTime, TimeSpan endTime);
        bool DeleteEmployeeSchedule(int id);
        bool IsLastScheduleSlot(int saloonEmployeeScheduleId);

        ICollection<SaloonEmployeeSchedule> GetSaloonEmployeeSchedules_BySaloonEmployeeIdAndDayId(
            int saloonEmployeeId, int dayId);

        /// <summary>
        /// Get all schedule of employee associated with any saloon.
        /// </summary>
        /// <param name="saloonEmployeeId">An array of saloon employee id</param>
        /// <returns></returns>
        ICollection<SaloonEmployeeSchedule> GetSaloonEmployeeSchedulesBySaloonEmployeeIds(
            int[] saloonEmployeeId);

    }
}
