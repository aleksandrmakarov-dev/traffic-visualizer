using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class GmailMailingService : IMailingService
    {
        private readonly MailingOptions _options;

        public GmailMailingService(IOptions<MailingOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_options.Username));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_options.Smtp, _options.Ssl);
            await smtp.AuthenticateAsync(_options.Username, _options.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
