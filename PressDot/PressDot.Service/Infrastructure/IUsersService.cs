using PressDot.Core;
using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface IUsersService : IService<Core.Domain.Users>
    {

        /// <summary>
        /// Check user exist against email.
        /// </summary>
        /// <param name="email">A valid email</param>
        /// <returns>Return true or false</returns>
        bool UserExist(string email);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">A valid email</param>
        /// <returns>Returns user object if found</returns>
        Users GetUserByEmail(string email);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">New password</param>
        /// <param name="passwordSalt">New salt</param>
        /// <returns></returns>
        bool ChangePassword(int userId, string password, string passwordSalt);

        IPagedList<Users> GetUsers(string name, int? userRoleId, int pageIndex, int pageSize);
        //Get user by user name
        Users GetUserByName(string userName);
        /// <summary>
        /// Get and delete user by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteUser(Users user);
        /// <summary>
        /// Return users by role assigned.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Users> GetUsersByRoleId(int roleId, string userName = "");
        List<Users> GetDentistUsers();
    }
}
