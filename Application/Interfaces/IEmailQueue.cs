using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmailQueue
    {
        void Enqueue(EmailMessage message);
        ValueTask<EmailMessage?> DequeueAsync(CancellationToken ct);
    }
}
