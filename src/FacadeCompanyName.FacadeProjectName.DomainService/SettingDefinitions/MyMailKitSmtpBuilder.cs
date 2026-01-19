using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using MailKit.Net.Smtp;

namespace FacadeCompanyName.FacadeProjectName.DomainService.SettingDefinitions
{
    public class MyMailKitSmtpBuilder : DefaultMailKitSmtpBuilder
    {
        public MyMailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, IAbpMailKitConfiguration abpMailKitConfiguration)
            : base(smtpEmailSenderConfiguration, abpMailKitConfiguration)
        {
        }

        public override SmtpClient Build()
        {
            var client = new SmtpClient();

            try
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                ConfigureClient(client);
                return client;
            }
            catch
            {
                client.Dispose();
                throw;
            }
        }
    }
}
