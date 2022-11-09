using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace PressDot.Core.Configuration
{
    /// <summary>
    /// Represents startup PressDot configuration parameters
    /// </summary>
    public partial class PressDotConfig
    {
        public string DataConnectionString { get; set; }

        public int PasswordSaltLength { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SenderEmail { get; set; }

        public string SenderPassword { get; set; }

        public string FromEmail { get; set; }

        public string EnableSsl { get; set; }

        public string SecretKeyForToken { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
