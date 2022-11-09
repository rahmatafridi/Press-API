using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Model
{
    public class NewRegistrationModel
    {
        public string Firstname { get; set; }

        public string Token { get; set; }

        public string WebsiteUrl { get; set; }

        public bool SelfRegistration { get; set; }

        public int UserRoleId { get; set; }
    }
}
