using MailKit.Net.Smtp;
using MailKit.Security;
using Wemuda_book_app.Configuration;
using Wemuda_book_app.Model;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Wemuda_book_app.Service
{
    public interface IMailService
    {
        Task<bool> SendAsync(Email email, CancellationToken cancelToken);
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendAsync(Email email, CancellationToken cancelToken = default)
        {
            try
            {
                var message = new MimeMessage();
                Console.WriteLine(_settings);

                #region Add Sender
                Console.WriteLine("From: " + _settings.DisplayName + "  (" + (email.From ?? _settings.From) + ")");
                message.From.Add(new MailboxAddress(_settings.DisplayName, email.From ?? _settings.From));
                
                Console.WriteLine("Sender: " + (email.DisplayName ?? _settings.DisplayName) + "  (" + (email.From ?? _settings.From) + ")");
                message.Sender = new MailboxAddress(email.DisplayName ?? _settings.DisplayName, email.From ?? _settings.From);
                #endregion

                #region Add Recievers
                Console.WriteLine("To:");
                foreach (string mailAddress in email.To)
                {
                    Console.WriteLine("  " + mailAddress);
                    message.To.Add(MailboxAddress.Parse(mailAddress));
                }

                if (!string.IsNullOrEmpty(email.ReplyTo))
                {
                    Console.WriteLine("Reply To: " + email.ReplyToName + "  (" + email.ReplyTo + ")");
                    message.ReplyTo.Add(new MailboxAddress(email.ReplyToName, email.ReplyTo));
                }

                if (email.Bcc != null)
                {
                    Console.WriteLine("BCC:");
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in email.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        Console.WriteLine("  " + mailAddress.Trim());
                        message.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }
                
                if (email.Cc != null)
                {
                    Console.WriteLine("CC:");
                    foreach (string mailAddress in email.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        Console.WriteLine("  " + mailAddress.Trim());
                        message.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }
                #endregion

                #region Add Content
                var body = new BodyBuilder();
                
                Console.WriteLine("Subject: " + email.Subject);
                message.Subject = email.Subject;
                
                Console.WriteLine("Body:");
                Console.WriteLine(email.Body);
                body.HtmlBody = email.Body;
                
                message.Body = body.ToMessageBody();
                #endregion

                #region Send Email
                Console.WriteLine("Cancellation Token: " + cancelToken);
                using var smtp = new SmtpClient();
                
                if (_settings.UseSSL)
                {
                    Console.WriteLine("Attempting to connect using SSL...");
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, cancelToken);
                    Console.WriteLine("Connected using SSL");
                }
                else if (_settings.UseStartTls)
                {
                    Console.WriteLine("Attempting to connect StartTLS...");
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, cancelToken);
                    Console.WriteLine("Connected using StartTLS");
                }

                Console.WriteLine("Attempting to authenticate with: " + _settings.UserName + ", " + _settings.Password);
                await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, cancelToken);
                Console.WriteLine("Authentication successful");

                Console.WriteLine("Attempting to send mail...");
                await smtp.SendAsync(message, cancelToken);
                Console.WriteLine("Mail sent successfully");

                await smtp.DisconnectAsync(true, cancelToken);
                #endregion

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}