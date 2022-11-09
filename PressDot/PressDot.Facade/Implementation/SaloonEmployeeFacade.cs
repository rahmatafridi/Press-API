using PressDot.Contracts.Request.SaloonEmployeeSchedule;
using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.SaloonEmployeeSchedule;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Facade.Implementation
{
    public class SaloonEmployeeFacade : ISaloonEmployeeFacade
    {
        #region private

        private readonly ISaloonEmployeeService _saloonEmployeeService;

        private readonly ISaloonService _saloonService;

        private readonly IUsersService _usersService;

        private readonly ISaloonEmployeeScheduleService _SaloonEmployeeScheduleService;

        private readonly IDaysOfWeekService _daysOfWeekService;

        #endregion


        #region ctor

        public SaloonEmployeeFacade(ISaloonEmployeeService saloonEmployeeService, ISaloonService saloonService, IUsersService usersService,
            ISaloonEmployeeScheduleService employeeScheduleService, IDaysOfWeekService daysOfWeekService)
        {
            _saloonEmployeeService = saloonEmployeeService;
            _saloonService = saloonService;
            _usersService = usersService;
            _SaloonEmployeeScheduleService = employeeScheduleService;
            _daysOfWeekService = daysOfWeekService;
        }

        #endregion

        #region methods



        public ICollection<SaloonEmployeeResponse> GetSaloonEmployeesByEmployeeId(int employeeId)
        {
            var saloonEmployees = _saloonEmployeeService.GetSaloonEmployeesByEmployeeId(employeeId);

            if (saloonEmployees == null)
                return null;
            var employees = _usersService.Get(saloonEmployees.Select(x => x.EmployeeId).ToArray());
            var saloons = _saloonService.Get(saloonEmployees.Select(x => x.SaloonId).ToArray());
            foreach (var saloonEmployee in saloonEmployees)
            {
                saloonEmployee.Employee = employees.FirstOrDefault(x => x.Id == saloonEmployee.EmployeeId);
                saloonEmployee.Saloon = saloons.FirstOrDefault(x => x.Id == saloonEmployee.SaloonId);
            }

            return saloonEmployees.ToModel<SaloonEmployeeResponse>();
        }

        public ICollection<GetSaloonEmployeeScheduleResponse> GetSaloonEmployeesBySaloonId(int saloonId)
        {
            var saloonEmployees = _saloonEmployeeService.GetSaloonEmployeesBySaloonId(saloonId);

            var listToReturn = new List<GetSaloonEmployeeScheduleResponse>();
            if (saloonEmployees == null)
                return null;
            var employees = _usersService.Get(saloonEmployees.Select(x => x.EmployeeId).ToArray());
            var saloons = _saloonService.Get(saloonEmployees.Select(x => x.SaloonId).ToArray());
            if (employees.Count > 0)
            {
                foreach (var saloonEmployee in saloonEmployees)
                {
                    saloonEmployee.Employee = employees.FirstOrDefault(x => x.Id == saloonEmployee.EmployeeId);
                    saloonEmployee.Saloon = saloons.FirstOrDefault(x => x.Id == saloonEmployee.SaloonId);

                    var employeeSchedule = _SaloonEmployeeScheduleService.Get()
                        .Where(x => x.SaloonEmployeeId == saloonEmployee.Id)
                        .ToModel<GetSaloonEmployeeScheduleResponse>().ToList();

                    foreach (var item in employeeSchedule)
                    {
                        item.DayName = _daysOfWeekService.GetDayById(item.Day);
                        item.EmployeeName = saloonEmployee.Employee.Firstname + " " + saloonEmployee.Employee.Lastname;
                        item.SaloonId = saloonEmployee.SaloonId;
                    }



                    listToReturn.AddRange(employeeSchedule);

                }
            }

            return listToReturn;
        }

        //This method will associate given employee object with saloon for the days that are being passed as an array
        public GetSaloonEmployeeScheduleResponse AttachEmployeeWithSaloonForDaysOfWeek(SaloonEmployeeScheduleCreateRequest employeeSchedule)
        {

            var toReturn = new GetSaloonEmployeeScheduleResponse();
            int saloonId = employeeSchedule.SaloonId, employeeId = employeeSchedule.EmployeeId;


            var saloon = _saloonService.Get(saloonId);
            if (saloon == null)
            {
                throw new PressDotNotFoundException("Saloon not found.");

            }

            var employee = _usersService.Get(employeeId);
            if (employee == null)
            {
                throw new PressDotNotFoundException("Employee not found.");

            }

            var saloonEmployees = _saloonEmployeeService.GetSaloonEmployeesByEmployeeId(employeeId);

            var saloonEmployee = saloonEmployees.FirstOrDefault(x => x.SaloonId == saloonId);

            

            if (saloonEmployee == null)
            {
                saloonEmployee = new SaloonEmployee()
                {
                    SaloonId = saloonId,
                    EmployeeId = employeeId,
                    IsDeleted = false,
                };
                _saloonEmployeeService.Create(saloonEmployee);

            }

            try
            {
                var existingEmployeeSchedules= _SaloonEmployeeScheduleService.GetSaloonEmployeeSchedulesBySaloonEmployeeIds(saloonEmployees
                    .Select(x => x.Id).ToArray());
                if(existingEmployeeSchedules.Any(x=>x.Day == employeeSchedule.Day
                                                 && ((x.StartTime >= employeeSchedule.StartTime && x.EndTime <= employeeSchedule.EndTime)
                                                     || (employeeSchedule.StartTime < x.StartTime && employeeSchedule.EndTime >= x.StartTime && employeeSchedule.EndTime <= x.EndTime)
                                                     || (employeeSchedule.StartTime < x.StartTime && employeeSchedule.EndTime >= x.EndTime)
                                                     || (employeeSchedule.StartTime > x.StartTime && employeeSchedule.StartTime <= x.EndTime)
                                                     || (employeeSchedule.EndTime >= x.StartTime && employeeSchedule.EndTime <= x.EndTime))))
                {
                    //already scheduled.
                    throw new PressDotException((int) SaloonEmployeeExceptionsCodes.Employee_Already_Scheduled,
                        "Employee is already associated with saloon for given day and time.");
                }

                var entity = employeeSchedule.ToEntity<SaloonEmployeeSchedule>();
                entity.IsDeleted = false;
                entity.CreatedDate = DateTime.Now;
                entity.SaloonEmployeeId = saloonEmployee.Id;
                _SaloonEmployeeScheduleService.Create(entity);
                toReturn = entity.ToModel<GetSaloonEmployeeScheduleResponse>();
                employeeSchedule.Success = true;



            }
            catch (PressDotException e)
            {

                throw e;
            }
            //}

            return toReturn;
        }

        public bool DeleteEmployeeSchedule(int saloonEmployeeScheduleId)
        {
            var result = false;

            var saloonEmployee = _SaloonEmployeeScheduleService.Get(saloonEmployeeScheduleId);

            if (saloonEmployee != null)
            {
                var isLastSlot = _SaloonEmployeeScheduleService.IsLastScheduleSlot(saloonEmployeeScheduleId);
                result = _SaloonEmployeeScheduleService.DeleteEmployeeSchedule(saloonEmployeeScheduleId);

                if (isLastSlot)
                {
                    _saloonEmployeeService.DeleteSaloonEmployeeSchedule(saloonEmployee.Id);
                }
            }

            return result;
        }

        public ICollection<SaloonEmployeeResponse> GetSaloonAdministratorsBySaloonId(int saloonId)
        {
            var administrators = _saloonEmployeeService.GetSaloonAdministrators(saloonId);
            if (administrators == null)
                return null;
            var employees = _usersService.Get(administrators.Select(x => x.EmployeeId).ToArray());
            foreach (var administrator in administrators)
            {
                administrator.Employee = employees.FirstOrDefault(x => x.Id == administrator.EmployeeId);
            }

            return administrators.ToModel<SaloonEmployeeResponse>();
        }

        public bool DeleteSaloonEmployee(int saloonEmployeeId)
        {
            var saloonEmployee = _saloonEmployeeService.Get(saloonEmployeeId);

            if(saloonEmployee == null)
                throw new PressDotNotFoundException("Invalid request to delete saloon and employee association.");
            _saloonEmployeeService.Remove(saloonEmployee);
            return true;
        }

        #endregion
    }
}
