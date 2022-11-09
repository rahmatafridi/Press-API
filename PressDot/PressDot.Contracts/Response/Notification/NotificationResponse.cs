using PressDot.Contracts.Response.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Notification
{
    public class NotificationResponse : BasePressDotEntityModel
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsAppointmentMadeStatus { get; set; }
        public bool IsAppointmentCompletedStatus { get; set; }
        public int Type { get; set; }
        public UsersResponse Users { get; set; }

    }
}
