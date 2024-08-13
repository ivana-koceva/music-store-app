using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpServerPort { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailDisplayName { get; set; }
        public string SendersName { get; set; }

        public EmailSettings() { }

        public EmailSettings(string smtpServer, string smptUserName, string smtpPassword, int smtpServerPort)
        {
            this.SmtpServer = smtpServer;
            this.SmtpUserName = smptUserName;
            this.SmtpPassword = smtpPassword;
            this.SmtpServerPort = smtpServerPort;
        }
    }
}
