using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface ISaloonEmployeeService : IService<SaloonEmployee>
    {
        /// <summary>
        /// Get saloon employee list
        /// </summary>
        /// <param name="saloonId">saloon id</param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetSaloonEmployeesBySaloonId(int saloonId);


        /// <summary>
        /// Get saloon list where given employee is working
        /// </summary>
        /// <param name="employeeId">employee id or user id</param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetSaloonEmployeesByEmployeeId(int employeeId);

        bool DeleteSaloonEmployeeSchedule(int saloonEmployeeId);

        /// <summary>
        /// Get saloon employee ids by saloon id
        /// </summary>
        /// <param name="saloonId"></param>
        /// <returns></returns>
        int[] GetSaloonEmployeeIdsBySaloonId(int saloonId);

        /// <summary>
        /// Get saloon employee by ids
        /// </summary>
        /// <param name="saloonEmployeeIds"></param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetSaloonEmployeesByIds(int[] saloonEmployeeIds);

        /// <summary>
        /// Get all saloon employees
        /// </summary>
        /// <param name="saloonId">saloon id</param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetAllSaloonEmployees(int saloonId);

        /// <summary>
        /// Get saloon employee by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SaloonEmployee GetSaloonEmployeeByUserId(int userId);
        /// <summary>
        /// Get saloon administrators for saloon
        /// </summary>
        /// <param name="saloonId">Saloon Id</param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetSaloonAdministrators(int saloonId);

        /// <summary>
        /// Get saloon administrators for saloon
        /// </summary>
        /// <param name="saloonIds">Array of Saloon Id</param>
        /// <returns></returns>
        ICollection<SaloonEmployee> GetSaloonEmployees(int[] saloonIds);


        bool DeleteSaloonEmployeeAssociation(SaloonEmployee saloonEmployee);

    }
}
