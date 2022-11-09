using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Model
{
    public class ForgotPasswordModel
    {
        public string Firstname { get; set; }

        public string Token { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
