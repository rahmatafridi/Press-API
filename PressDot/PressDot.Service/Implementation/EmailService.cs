using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using PressDot.Core.Configuration;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class EmailService : IEmailService
    {
        #region private

        private readonly PressDotConfig _pressDotConfig;

        #endregion

        #region ctor

        public EmailService(PressDotConfig pressDotConfig)
        {
            _pressDotConfig = pressDotConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="subject">Mail subject</param>
        /// <param name="message">Mail body</param>
        /// <param name="receiverEmail">Receiver Email. If there are multiple receivers, then set this parameter as abc@domain.com;xyz@domain.com</param>
        public void SendEmail(string subject, string message,string receiverEmail)
        {
            var mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient(_pressDotConfig.SmtpServer, _pressDotConfig.SmtpPort);
            smtp.UseDefaultCredentials = true;
            mailMessage.From = new MailAddress(_pressDotConfig.FromEmail);
            if (!String.IsNullOrEmpty(receiverEmail))
            {
                var emailArray = receiverEmail.Split(new[] { ';' });
                foreach (string email in emailArray)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mailMessage.To.Add(email);
                    }
                }
            }

            smtp.Credentials = new System.Net.NetworkCredential(_pressDotConfig.SenderEmail, _pressDotConfig.SenderPassword);
            smtp.EnableSsl = Convert.ToBoolean(_pressDotConfig.EnableSsl);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            smtp.Send(mailMessage);
        }

        #endregion
    }
}
