using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Customer
{
    public class ChangePasswordByTokenRequest : BasePressDotEntityModel
    {
        public string Token { get; set; }

        public string Password { get; set; }
    }
}
