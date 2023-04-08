using Mailjet.Client;
using Newtonsoft.Json.Linq;
using TestCQRS.Common.Email.EmailSender;

namespace TestCQRS.Common.Email.EmailProvider
{
    public class MailJetProvider : EmailSender.EmailSender, IEmailSender
    {
        protected override async Task  Send(EmailModel email)
        {
			try
			{
				JArray jArray= new JArray();
				JArray attachments = new JArray();
				if (email.Attachments != null && email.Attachments.Count() > 0)
				{
					email.Attachments.ToList().ForEach(attachment => attachments.Add(new JObject
					{
						new JProperty("Content-Type", attachment.ContentType),
						new JProperty("Filename", attachment.Name),
						new JProperty("Content", Convert.ToBase64String(attachment.Data))
					}));
				}
				jArray.Add(new JObject
				{
					new JProperty("FromEmail", "longdv265@gmail.com"), //email register mailjet
					new JProperty("FromName", "Longdv"),
					new JProperty("Recipients", new JArray
					{
						new JObject
						{
							new JProperty("Email", email.EmailAddress),
							new JProperty("Name", email.EmailAddress)
						}
					}),
					new JProperty("Subject", email.Subject),
					new JProperty("Text-part", email.Body),
					new JProperty("Html-part", email.Body), // use html format
					new JProperty("Attachments", attachments)
				});
				var client = EmailSender.EmailSender.CreateMailJetClient();
				var request = new MailjetRequest
				{
					Resource = Mailjet.Client.Resources.Send.Resource
				}
				.Property(Mailjet.Client.Resources.Send.Messages, jArray);
				var response = await client.PostAsync(request);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
			}
        }
    }
}
