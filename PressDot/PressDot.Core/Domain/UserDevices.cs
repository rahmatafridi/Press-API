using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class UserDevices : BaseEntity
    {
        public int UserId { get; set; }

        public string DeviceToken { get; set; }

        public virtual Users Users { get; set; }

        public string DeviceId { get; set; }
    }
}
