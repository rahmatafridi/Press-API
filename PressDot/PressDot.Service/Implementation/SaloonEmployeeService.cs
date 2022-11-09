using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;

namespace PressDot.Service.Implementation
{
    public class SaloonEmployeeService : BaseService<SaloonEmployee>, ISaloonEmployeeService
    {
        #region private

        private readonly IRepository<Users> _userRepository;
        #endregion

        #region ctor

        public SaloonEmployeeService(IRepository<SaloonEmployee> saloonEmployeeRepository, IRepository<Users> userRepository) : base(saloonEmployeeRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region methods

        public ICollection<SaloonEmployee> GetSaloonEmployeesBySaloonId(int saloonId)
        {
            return Repository.Table.Where(x => x.SaloonId == saloonId).ToList();
        }

        public ICollection<SaloonEmployee> GetSaloonEmployeesByIds(int[] saloonEmployeeIds)
        {
            return Repository.Table.Where(x => saloonEmployeeIds.Contains(x.Id)).ToList();
        }

        public int[] GetSaloonEmployeeIdsBySaloonId(int saloonId)
        {
            var ids = Repository.Table.Where(x => x.SaloonId == saloonId).Select(s => s.Id).ToArray();
            return ids;
        }

        public ICollection<SaloonEmployee> GetSaloonEmployeesByEmployeeId(int employeeId)
        {
            return Repository.Table.Where(x => x.EmployeeId == employeeId).ToList();
        }

        public bool DeleteSaloonEmployeeSchedule(int saloonEmployeeId)
        {
            var dbObj = Repository.Table.FirstOrDefault(x => x.Id == saloonEmployeeId);
            if (dbObj != null)
            {
                dbObj.DeletedDate = DateTime.Now;
                dbObj.IsDeleted = true;
                Repository.Update(dbObj);
                return true;
            }

            return false;
        }

        public ICollection<SaloonEmployee> GetAllSaloonEmployees(int saloonId)
        {
            return Repository.Table.Where(x => x.SaloonId == saloonId).ToList();
        }

        public SaloonEmployee GetSaloonEmployeeByUserId(int userId)
        {
            return Repository.Table.FirstOrDefault(x => x.EmployeeId == userId);
        }

        public ICollection<SaloonEmployee> GetSaloonAdministrators(int saloonId)
        {
            return (from se in Repository.Table
                join u in _userRepository.Table on se.EmployeeId equals u.Id
                where u.UserRoleId == 6 && se.SaloonId == saloonId
                select se).ToList();
        }

        public ICollection<SaloonEmployee> GetSaloonEmployees(int[] saloonIds)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId)).ToList();
        }

        public bool DeleteSaloonEmployeeAssociation(SaloonEmployee saloonEmployee)
        {
            try
            {
                saloonEmployee.IsDeleted = true;
                saloonEmployee.DeletedDate = DateTime.Now;
                Repository.Update(saloonEmployee);
                return true;

            }
            catch (Exception e)
            {
                throw new PressDotException((int)SaloonEmployeeExceptionsCodes.Some_Thing_Went_Wrong_While_deleting_Employee_Association_With_Saloon,
                    "Something went wrong while employee association with saloon.");
            }
        }
        #endregion
    }
}
