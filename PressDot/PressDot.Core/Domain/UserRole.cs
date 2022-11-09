using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class UserRole : BaseEntity
    {
        public string UserRoleName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
