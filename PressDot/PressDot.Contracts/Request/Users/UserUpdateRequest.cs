using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Users
{
    public class UserUpdateRequest : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public bool IsActive { get; set; }

        public int UserRoleId { get; set; }
    }
}
