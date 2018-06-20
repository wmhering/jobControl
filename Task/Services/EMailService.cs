using System.Net.Mail;

namespace JobControl.Task.Services
{
    public class EMailService : IEMailService
    {
        private SmtpClient _EMailClient;
        private string _SenderEMailAddress;

        public EMailService(ConfigurationData configurationData)
        {
            _EMailClient = new SmtpClient(configurationData.SmtpHost, configurationData.SmtpPort);
            _SenderEMailAddress = configurationData.SenderEMailAddress;
        }

        public void SendMessage(string emailAddresses, string subject, string message)
        {
            _EMailClient.Send(_SenderEMailAddress, emailAddresses, subject, message);
        }
    }
}
