namespace TestCQRS.Common.Email.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmail(string address, string subject, string body, List<EmailAttachment>? emailAttachment = null);
        Task SendEmail(EmailModel emailModel);
    }
}
