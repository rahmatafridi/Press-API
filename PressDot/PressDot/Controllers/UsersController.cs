using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Users;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Users")]
    public class UsersController :AuthenticatedController
    {
        #region Private Members

        private readonly IUsersFacade _usersFacade;

        private readonly IUsersService _usersService;

        private readonly IUserRoleService _roleService;

        private readonly IMessagingService _messagingService;
        #endregion

        #region ctor

        public UsersController(IUsersFacade customerFacade, IUsersService customerService
            , IUserRoleService userRoleService,IMessagingService messagingService)
        {
            _usersFacade = customerFacade;
            _usersService = customerService;
            _roleService = userRoleService;
            _messagingService = messagingService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll/")]
        public IActionResult GetAll()
        {
            return Ok(_usersFacade.GetUsers());
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsers/")]
        public IActionResult GetUsers(string name, int? userRoleId, int pageIndex = 0, int pageSize = 10)
        {
            return Ok(_usersFacade.GetUsers(name, userRoleId, pageIndex, pageSize));
        }

        [HttpGet]
        [Route("GetUserRolesByRoleIds")]
        public IActionResult GetUserRoelsByRoleIds(int roleId, string userName = "")
        {
            var result = _usersFacade.GetUsersByRoles(roleId,userName);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetDentistUsers")]
        public IActionResult GetDentistUsers()
        {
            var result = _usersFacade.GetDentistUsers();
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            var result = _usersFacade.DeleteUser(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            var result = _usersFacade.GetUserById(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult UpdateSaloon(UserUpdateRequest request)
        {
            var result = _usersFacade.UpdateUser(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveUserDevice")]
        public ActionResult SaveUserDevice(UserDeviceRequest userDeviceRequest)
        {
            var result = _usersFacade.SaveUserDevice(userDeviceRequest);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserDeviceToken")]
        public IActionResult GetUserDeviceToken(int userId,string deviceId)
        {
            var result = _usersFacade.GetUserDeviceByUserIdAndDeviceId(userId,deviceId);
            return Ok(result);
        }

        [HttpPost]
        [Route("SendMessage")]
        public ActionResult SendMessage(string token,string title, string body,int userId)
        {
            var result = _messagingService.SendNotification(token,title,body,userId);
            return Ok(result);
        }
        #endregion
    }
}
