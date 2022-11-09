using PressDot.Contracts.Request.SaloonEmployeeSchedule;
using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.SaloonEmployeeSchedule;
using System.Collections.Generic;

namespace PressDot.Facade.Infrastructure
{
    public interface ISaloonEmployeeFacade
    {
        /// <summary>
        /// Get saloon employee list
        /// </summary>
        /// <param name="saloonId">saloon id</param>
        /// <returns></returns>
        ICollection<GetSaloonEmployeeScheduleResponse> GetSaloonEmployeesBySaloonId(int saloonId);


        /// <summary>
        /// Get saloon list where given employee is working
        /// </summary>
        /// <param name="employeeId">employee id or user id</param>
        /// <returns></returns>
        ICollection<SaloonEmployeeResponse> GetSaloonEmployeesByEmployeeId(int employeeId);

        /// <summary>
        /// attach employee with saloon for days of week
        /// </summary>
        /// <param name="scheduleList"></param>
        /// <returns></returns>
        GetSaloonEmployeeScheduleResponse AttachEmployeeWithSaloonForDaysOfWeek(SaloonEmployeeScheduleCreateRequest scheduleList);

        bool DeleteEmployeeSchedule(int saloonEmployeeScheduleId);

        /// <summary>
        /// Delete saloon employee relation
        /// </summary>
        /// <param name="saloonEmployeeId"></param>
        /// <returns></returns>
        bool DeleteSaloonEmployee(int saloonEmployeeId);

        ICollection<SaloonEmployeeResponse> GetSaloonAdministratorsBySaloonId(int saloonId);
    }
}
