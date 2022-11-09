using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using PressDot.Contracts.Request.Customer;
using PressDot.Contracts.Response;
using PressDot.Contracts.Response.Appointment;
using PressDot.Contracts.Response.Users;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Framework.Attribute;
using PressDot.Service.Infrastructure;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v2;

namespace PressDot.Controllers
{
    [Route("api/v1/Account")]
    public class AccountController : BaseController
    {
        #region private

        private readonly IUsersFacade _usersFacade;
        private readonly IAppointmentFacade _appointmentFacade;

        private readonly IUserRoleService _usersRoleService;
        #endregion

        #region ctor

        public AccountController(IAppointmentFacade appointmentFacade,IUsersFacade usersFacade, IUserRoleService usersRoleService)
        {
            _usersFacade = usersFacade;
            _usersRoleService = usersRoleService;
            _appointmentFacade = appointmentFacade;

        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Register")]
        public IActionResult Create(RegisterUsersRequest request)
        {
            return Ok(_usersFacade.RegisterUser(request));
        }

        //[Authorize]
        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            return Ok(_usersRoleService.Get().ToModel<UsersRoleResponse>());
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(string username,string password)
        {
            return Ok(_usersFacade.AuthenticateUser(username,password));
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            return Ok(_usersFacade.ChangePassword(changePasswordRequest.UserId, changePasswordRequest.Password,changePasswordRequest.OldPassword));
        }

        [HttpPost]
        [Route("ChangePasswordByToken")]
        public IActionResult ChangePasswordByToken(ChangePasswordByTokenRequest changePasswordByTokenRequest)
        {
            return Ok(_usersFacade.ChangePassword(changePasswordByTokenRequest.Token, changePasswordByTokenRequest.Password));
        }

        [HttpPost]
        [Route("ActivateAccount")]
        public IActionResult ActivateAccount(string token)
        {
            return Ok(_usersFacade.ActivateAccount(token));
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            return Ok(_usersFacade.ForgotPassword(email));
        }
        [HttpGet]
        [Route("LoadProducts")]
        public async Task<ActionResult> LoadProducts()
        {
            MyRestAPI rest = new MyRestAPI("https://pressdots.com/wp-json/wc/v2/", "ck_c71ea5a3b10a5628e7d92fb01f9fe650d34dedf7", "cs_a274b733010d44592732b1766a4773005ee3721e");
            WCObject wc = new WCObject(rest);
            var products = await wc.Product.GetAll();
            var list = new List<ProducatResponse>();
            foreach (var item in products)
            {
                list.Add(new ProducatResponse()
                {
                    name= item.name,
                    description= item.description,
                    image= item.images[0].src,
                    link=item.permalink
                });
            }
            // var products = _appointmentFacade.LoadProducts();
            return Ok(list);
        }
        public class MyRestAPI : RestAPI
        {
            public MyRestAPI(string url, string key, string secret, bool authorizedHeader = true,
                Func<string, string> jsonSerializeFilter = null,
                Func<string, string> jsonDeserializeFilter = null,
                Action<HttpWebRequest> requestFilter = null) : base(url, key, secret, authorizedHeader, jsonSerializeFilter, jsonDeserializeFilter, requestFilter)
            {
            }

            public override T DeserializeJSon<T>(string jsonString)
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            public override string SerializeJSon<T>(T t)
            {
                return JsonConvert.SerializeObject(t);
            }
        }
        #endregion
    }
}
