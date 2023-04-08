using System.Threading.Tasks.Dataflow;
using TestCQRS.Common.Email;
using TestCQRS.Common.Email.EmailProvider;
using TestCQRS.Common.Email.EmailSender;

namespace TestCQRS.HostedServices
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        private Task? _sendTask;
        private CancellationTokenSource? _cancellationToken;
        private readonly BufferBlock<EmailModel> _mailQueue;
        private readonly IEmailSender _mailSender;

        public EmailHostedService()
        {
            _mailQueue = new BufferBlock<EmailModel>();
            _cancellationToken = new CancellationTokenSource();
            _mailSender = new MailJetProvider();
        }
        //wakeup bufferblock
        public async Task SendEmailAsync(EmailModel emailModel) => await _mailQueue.SendAsync(emailModel);
        public void Dispose()
        {
            DestroyTask();
        }

        private void DestroyTask()
        {
            try
            {
                if (_cancellationToken != null)
                {
                    _cancellationToken.Cancel();
                    _cancellationToken = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("[Email Service] Start");
            _sendTask = BackgroundSendMailAsync(_cancellationToken!.Token);
            return Task.CompletedTask;
        }

        private async Task? BackgroundSendMailAsync(CancellationToken token)
        {
            //Listen when have singal from bufferblock
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var email = await _mailQueue.ReceiveAsync();
                    await _mailSender.SendEmail(email);
                }
                catch (OperationCanceledException ex)
                {
                    break;
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DestroyTask();
            await Task.WhenAny(_sendTask!, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
}
