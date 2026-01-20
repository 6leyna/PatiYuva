using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace PatiYuva.Services
{
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;
        public static ConcurrentDictionary<string, string> LastVerificationCodes { get; set; } = new ConcurrentDictionary<string, string>();

        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Extract the code from the message
            var code = ExtractCodeFromMessage(message);
            
            // Store the code for the email address
            LastVerificationCodes[email] = code;
            
            // In a real application, this would send an actual email
            // For now, we'll just log it
            _logger.LogInformation($"EMAIL SENT TO: {email}");
            _logger.LogInformation($"SUBJECT: {subject}");
            _logger.LogInformation($"MESSAGE: {message}");
            
            // Simulate async operation
            await Task.CompletedTask;
        }
        
        private string ExtractCodeFromMessage(string message)
        {
            // Extract the 6-digit code from the message
            var parts = message.Split(' ');
            foreach(var part in parts)
            {
                if(part.Length == 6 && int.TryParse(part, out _))
                {
                    return part;
                }
            }
            return "";
        }
    }
}