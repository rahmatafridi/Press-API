using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace PressDot.Core.Domain
{
    public class Users : BaseEntity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int UserRoleId { get; set; }

        public bool IsActive { get; set; }

        public virtual UserRole UsersRole { get; set; }

        public virtual ICollection<SaloonEmployee> SaloonEmployees { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}
