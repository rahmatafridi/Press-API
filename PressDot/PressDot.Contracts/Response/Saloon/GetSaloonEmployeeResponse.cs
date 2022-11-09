using PressDot.Contracts.Response.SaloonEmployeeSchedule;
using System.Collections.Generic;

namespace PressDot.Contracts.Response.Saloon
{
    public class GetSaloonEmployeeResponse : BasePressDotEntityModel
    {

        public List<GetSaloonEmployeeScheduleResponse> EmployeeSchedule { get; set; }

    }
}
