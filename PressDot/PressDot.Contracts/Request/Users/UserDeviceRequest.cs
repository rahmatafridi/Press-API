using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Users
{
    public class UserDeviceRequest : BasePressDotEntityModel
    {
        public int UserId { get; set; }

        public string DeviceToken { get; set; }

        public string DeviceId { get; set; }
    }
}
