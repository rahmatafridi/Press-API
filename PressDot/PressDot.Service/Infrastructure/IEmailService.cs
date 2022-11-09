using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Service.Infrastructure
{
    public interface IEmailService
    {
        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="subject">Mail subject</param>
        /// <param name="message">Mail body</param>
        /// <param name="receiverEmail">Receiver Email. If there are multiple receivers, then set this parameter as abc@domain.com;xyz@domain.com</param>
        void SendEmail(string subject, string message, string receiverEmail);
    }
}
