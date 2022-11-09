using System;
using System.ComponentModel.DataAnnotations;

namespace PressDot.Contracts.Request.SaloonEmployeeSchedule
{
    public class SaloonEmployeeScheduleCreateRequest : BasePressDotEntityModel
    {
        public int SaloonEmployeeId { get; set; }
        public int SaloonId { get; set; }
        public int EmployeeId { get; set; }
        public int Day { get; set; }
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        public bool Success { get; set; }
    }
}
