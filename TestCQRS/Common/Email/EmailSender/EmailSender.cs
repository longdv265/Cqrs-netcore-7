using Mailjet.Client;

namespace TestCQRS.Common.Email.EmailSender
{
    public abstract class EmailSender : IEmailSender
    {
        public static MailjetClient CreateMailJetClient()
        {
            return new MailjetClient("cae87d9cf7282d059d1c62c30dc391a1",
               "92c02241321a61f4aa3d24c3caead86d");
        }
        protected abstract Task Send(EmailModel email);

        public async Task SendEmail(EmailModel emailModel)
        {
            await Send(emailModel);
        }

        public async Task SendEmail(string address, string subject, string body, List<EmailAttachment>? emailAttachment = null)
        {
            await Send(new EmailModel
            {
                Attachments = emailAttachment!,
                Body = body,
                Subject = subject,
                EmailAddress = address,
            });
        }
    }
}
