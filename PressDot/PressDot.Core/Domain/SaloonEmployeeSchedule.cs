using System;
using System.ComponentModel.DataAnnotations;

namespace PressDot.Core.Domain
{
    public class SaloonEmployeeSchedule : BaseEntity
    {
        public int SaloonEmployeeId { get; set; }

        public int Day { get; set; }
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}
