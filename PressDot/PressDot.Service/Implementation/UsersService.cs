using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class UsersService : BaseService<Users>, IUsersService
    {
        #region private



        #endregion

        #region ctor

        public UsersService(IRepository<Users> customerRepository) : base(customerRepository)
        {

        }

        #endregion

        #region Methods

        public bool UserExist(string email)
        {
            return this.Repository.Table.Any(x => x.Email.Equals(email));
        }

        public Users GetUserByEmail(string email)
        {
            return this.Repository.Table.FirstOrDefault(x => x.Email.Equals(email));
        }


        public bool ChangePassword(int userId, string password, string passwordSalt)
        {
            var user = Get(userId);
            user.Password = password;
            user.PasswordSalt = passwordSalt;
            base.Update(user);
            return true;
        }

        public IPagedList<Users> GetUsers(string name, int? userRoleId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x =>
                    x.Firstname.Contains(name) || x.Lastname.Contains(name) ||
                    name.Contains(x.Firstname + " " + x.Lastname));
            if (userRoleId != null)
                query = query.Where(x => x.UserRoleId == userRoleId.Value);
            return new PagedList<Users>(query, pageIndex, pageSize);
        }

        public Users GetUserByName(string userName)
        {
            return Repository.Table.FirstOrDefault(x => (x.Firstname + " " + x.Lastname).Contains(userName.Trim()));
        }
      
        public bool DeleteUser(Users user)
        {
            try
            {
                user.IsDeleted = true;
                user.DeletedDate = DateTime.Now;
                Repository.Update(user);
                return true;

            }
            catch (Exception e)
            {
                throw new PressDotException((int) UserExceptionsCodes.Something_Went_Wrong_While_Deleting_User,
                    "Something went wrong while deleting user.");
            }
        }

        public List<Users> GetUsersByRoleId(int roleId, string userName = "")
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var users = Repository.Table.Where(x =>
                    (x.Firstname + " " + x.Firstname).ToLower().Contains(userName.Trim().ToLower())
                    && x.UserRoleId == roleId);

                return users.ToList();
            }
            else
            {
                var users = Repository.Table.Where(x => x.UserRoleId == roleId);

                return users.ToList();
            }

        }


        public List<Users> GetDentistUsers()
        {
            var users = Repository.Table.Where(x => x.UserRoleId == 3 || x.UserRoleId == 4);
            return users.ToList();

        }

        #endregion
    }
}

