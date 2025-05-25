using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
