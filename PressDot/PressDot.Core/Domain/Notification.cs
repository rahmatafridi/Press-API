using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public bool IsAppointmentMadeStatus { get; set; }
        public bool IsAppointmentCompletedStatus { get; set; }
        public int  Type { get; set; }
        public virtual Users Users { get; set; }

    }
}
