using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using TravelApp.ConfigurationOptions;

namespace TravelApp.HelperService
{
    public class SmtpMailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        private readonly MailOptions _mailOptions;

        public SmtpMailSender(IConfiguration configuration, IOptions<MailOptions> mailoptions)
        {
            _configuration = configuration;
            _mailOptions = mailoptions.Value;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject">Mail Başlık</param>
        /// <param name="body">İçerik</param>
        /// <param name="to">a@a.com,b@b.com,c@c.com.... </param>
        /// <returns></returns>
        public bool sendMailFORapp(string subject, string body, string to, string cc = "", string bcc = "")
        {


            string smtpClient = _mailOptions.SmtpClient; // _configuration["MailSettings:SmtpClient"];
            int smtpPort = _mailOptions.SmtpPort; //  _configuration.GetValue<int>("MailSettings:SmtpPort");
            string displayName = _mailOptions.DisplayName;//  _configuration.GetValue<string>("MailSettings:DisplayName");
            string SmtpCredentialUserName = _mailOptions.SmtpCredentialUserName;//  _configuration.GetValue<string>("MailSettings:SmtpCredentialUserName");
            string SmtpCredentialPassword = _mailOptions.SmtpCredentialPassword;// _configuration.GetValue<string>("MailSettings:SmtpCredentialPassword");

            string[] tof = to == null ? new string[] { SmtpCredentialUserName } : to.Split(',');
            string[] ccs = string.IsNullOrEmpty(cc) == true ? new string[] { } : cc.Split(",");
            string[] bccs = string.IsNullOrEmpty(bcc) == true ? new string[] { } : bcc.Split(",");

            return sendMail(smtpClient, smtpPort, new System.Net.NetworkCredential(SmtpCredentialUserName, SmtpCredentialPassword), displayName, tof, subject, body, ccs, bccs);
        }

        bool sendMail(string host, int port, NetworkCredential senderInfo, string senderdisplayName,
                            string[] to, string subject, string body, string[] ccs, string[] bccs)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(senderInfo.UserName, senderdisplayName);

            foreach (var item in to)
            {
                mail.To.Add(item);
            }

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            if (ccs.Any())
            {
                foreach (var item in ccs)
                {
                    mail.CC.Add(item);
                }
            }

            if (bccs.Any())
            {
                foreach (var item in bccs)
                {
                    mail.Bcc.Add(item);
                }

            }

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderInfo.UserName, senderInfo.Password)

            };

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
