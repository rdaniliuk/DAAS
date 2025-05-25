using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IEmailQueue _queue;
        private readonly IEmailSender _sender;
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(IEmailQueue queue,IEmailSender sender,ILogger<EmailBackgroundService> logger)
        {
            _queue = queue;
            _sender = sender;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var email = await _queue.DequeueAsync(stoppingToken);
                    if (email is not null)
                    {
                        await _sender.SendEmailAsync(email, stoppingToken);
                        _logger.LogInformation("Sent \"true\" email to {To}", email.To);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogError("Error in EmailBackgroundService");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in EmailBackgroundService");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            _logger.LogInformation("Email background service stopping.");
        }
    }

}
