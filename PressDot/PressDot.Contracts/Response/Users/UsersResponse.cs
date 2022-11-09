using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Users
{
    public class UsersResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public bool IsActive { get; set; }

        public int UserRoleId { get; set; }

        public UsersRoleResponse UsersRole { get; set; }
    }
}
