using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class ConsoleEmailSender : IEmailSender
    {
        private readonly ILogger<ConsoleEmailSender> _logger;

        public ConsoleEmailSender(ILogger<ConsoleEmailSender> logger)
            => _logger = logger;

        public Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"[TrueEmail] To:{message.To} Subject:{message.Subject} Body:{message.Body}");
            return Task.CompletedTask;
        }
    }
}
