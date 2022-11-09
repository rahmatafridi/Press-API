using PressDot.Contracts.Response.SaloonEmployeeSchedule;
using System.Collections.Generic;
using PressDot.Contracts.Response.Users;

namespace PressDot.Contracts.Response.Saloon
{
    public class SaloonEmployeeResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public int SaloonId { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public UsersResponse Employee { get; set; }

        public List<GetSaloonEmployeeScheduleResponse> EmployeeSchedule { get; set; }

    }
}
