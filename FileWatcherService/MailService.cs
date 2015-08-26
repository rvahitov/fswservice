using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using FileWatcherService.Configuration;
using FileWatcherService.Utils.TemplateValueProviders;

namespace FileWatcherService
{
    internal class MailService: IDisposable
    {
        private static readonly Regex TemplateKeyRegex = new Regex(@"{([^{}]+)}", RegexOptions.Compiled);
        private readonly SendMail _sendMailConfig;
        private readonly SmtpClient _smtpClient;

        public MailService(SendMail sendMailConfig)
        {
            _sendMailConfig = sendMailConfig;
            _smtpClient = new SmtpClient(_sendMailConfig.Server.Host, _sendMailConfig.Server.Port){EnableSsl = _sendMailConfig.Server.EnableSSL};
            if (string.IsNullOrWhiteSpace(_sendMailConfig.Server.Login))
            {
                _smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                _smtpClient.Credentials = new NetworkCredential(_sendMailConfig.Server.Login,
                    _sendMailConfig.Server.Password);
            }
        }

        public void SendMessage(FileSystemEventArgs args, string serviceName, string serviceStatus)
        {
            if(_sendMailConfig.Message.Count == 0) return;

            var valueSource = new TemplateValueSource(args, serviceName, serviceStatus);

            MatchEvaluator matchEvaluator = match =>
            {
                ITemplateValueProvider provider;
                var key = match.Groups[1].Value;
                return TemplateValueProviders.Instance.TryGet(key, out provider)
                    ? provider.GetTemplateValue(valueSource)
                    : match.ToString();
            };
            var subject = TemplateKeyRegex.Replace(_sendMailConfig.Message.SubjectTemplate, matchEvaluator);
            var body = TemplateKeyRegex.Replace(LoadBodyTemplate(), matchEvaluator);

            var message = new MailMessage
            {
                From = new MailAddress(_sendMailConfig.Message.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = _sendMailConfig.Message.IsBodyHtml
            };
            foreach (MailReceiverAddress receiver in _sendMailConfig.Message)
            {
                message.To.Add(receiver.Address);
            }

            _smtpClient.Send(message);
        }

        private string LoadBodyTemplate()
        {
            var fullPath = Path.GetFullPath(_sendMailConfig.Message.BodyTemplatePath);
            return File.ReadAllText(fullPath, Encoding.UTF8);
        }

        public void Dispose()
        {
            ((IDisposable) _smtpClient).Dispose();
        }
    }
}