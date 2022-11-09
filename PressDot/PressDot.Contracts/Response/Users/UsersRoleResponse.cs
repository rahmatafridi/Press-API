using System.Collections.Generic;

namespace PressDot.Contracts.Response.Users
{
    public class UsersRoleResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public string UserRoleName { get; set; }

        public ICollection<UsersResponse> Users { get; set; }

    }
}
