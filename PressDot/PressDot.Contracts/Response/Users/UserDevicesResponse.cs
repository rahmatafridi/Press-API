using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace PressDot.Contracts.Response.Users
{
    public class UserDevicesResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string DeviceToken { get; set; }

        public string DeviceId { get; set; }
    }
}
