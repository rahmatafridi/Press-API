using Microsoft.EntityFrameworkCore;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        #region Private


        private IUsersService _usersService;

        #endregion


        #region ctor

        public UserRoleService(IRepository<UserRole> usersRoleRepository, IUsersService usersService) : base(usersRoleRepository)
        {

            _usersService = usersService;
        }

        


        #endregion

        #region Methods

        public  bool IsValidRoleId(int roleId)
        {
            var role = Repository.Table.FirstOrDefault(x => x.Id == roleId);
            return role != null;
        }
        public UserRole GetRoleByName(string roleName)
        {
            return Repository.Table.FirstOrDefault(x => x.UserRoleName == roleName);
        }
        #endregion
    }
}
