using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Model
{
    public class AppointmentInfoToCustomer
    {
        public int AppointmentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string State { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
    }
}
