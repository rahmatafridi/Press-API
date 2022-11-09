using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Customer
{
    public class ChangePasswordRequest : BasePressDotEntityModel
    {
        public int UserId { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }
    }
}
