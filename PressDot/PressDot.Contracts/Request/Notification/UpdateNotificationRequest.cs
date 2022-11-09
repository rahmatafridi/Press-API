using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Notification
{
    public class UpdateNotificationRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsAppointmentMadeStatus { get; set; }
        public bool IsAppointmentCompletedStatus { get; set; }
    }
}
