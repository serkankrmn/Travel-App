namespace TravelApp.HelperService
{
    public interface IMailSender
    {
        bool sendMailFORapp(string subject, string body, string to, string cc = "", string bcc = "");

    }
}
