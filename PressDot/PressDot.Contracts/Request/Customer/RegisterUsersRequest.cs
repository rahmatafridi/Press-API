using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace PressDot.Contracts.Request.Customer
{
    public class RegisterUsersRequest : BasePressDotEntityModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }

        public int UserRoleId { get; set; }

        public bool SelfRegistration { get; set; }
    }
}
