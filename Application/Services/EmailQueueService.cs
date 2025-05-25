using Application.DTOs;
using Application.Interfaces;
using System.Threading.Channels;

namespace Application.Services
{
    public class EmailQueueService : IEmailQueue
    {
        private readonly Channel<EmailMessage> _channel = Channel.CreateUnbounded<EmailMessage>();

        public void Enqueue(EmailMessage message)
        {
            if (!_channel.Writer.TryWrite(message))
                throw new InvalidOperationException("Couldn't queue an email");
        }

        public async ValueTask<EmailMessage?> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
