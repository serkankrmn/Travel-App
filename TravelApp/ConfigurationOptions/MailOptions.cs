namespace TravelApp.ConfigurationOptions
{
    public class MailOptions
    {
        public const string MailSettings = "MailSettings";


        public string SmtpClient { get; set; }

        public int SmtpPort { get; set; }

        public string DisplayName { get; set; }

        public string SmtpCredentialUserName { get; set; }

        public string SmtpCredentialPassword { get; set; }
    }
}
