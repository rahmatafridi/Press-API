using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class Consent:BaseEntity
    {
        public int UserId { get; set; }
        public bool IsSent { get; set; }
    }
}
