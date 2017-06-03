using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Nidan.Business.Interfaces;
using SharedTypes.DataContracts;


namespace Nidan.Business
{
    public class EmailService : IEmailService
    {
        private readonly string[] _overrideEmailAddresses;

        public EmailService(string overrideEmailAddresses)
        {
            if (!string.IsNullOrEmpty(overrideEmailAddresses))
            {
                _overrideEmailAddresses = overrideEmailAddresses.Split(';');
            }
        }

        public void SendEmail(EmailData data)
        {
            UseOverrideEmailDataIfSet(data);
            try
            {
                var msg = CreateMessage(data);
                SendEmail(msg);
            }
            catch (Exception Ex)
            {
                // ignored
            }
        }

        //public void SendEmail(EmailData data, List<Guid> docGuids)
        //{
        //    UseOverrideEmailDataIfSet(data);

        //    var client = new EmailServiceClient();
        //    client.SendEmailWithAttachments(data, docGuids);
        //}

        public void SendEmail(EmailData data, Dictionary<string, byte[]> attachments)
        {
            try
            {
                var msg = CreateMessage(data);
                foreach (var attachment in attachments)
                {
                    msg.Attachments.Add(new Attachment(new MemoryStream(attachment.Value), attachment.Key));
                }
                SendEmail(msg);
            }
            catch (Exception Ex)
            {
                // ignored
            }
        }

        private void UseOverrideEmailDataIfSet(EmailData data)
        {
            if (_overrideEmailAddresses != null)
            {
                data.ToAddressList.Clear();
                data.ToAddressList.AddRange(_overrideEmailAddresses);
                if (data.CCAddressList != null)
                    data.CCAddressList.Clear();
                if (data.BCCAddressList != null)
                    data.BCCAddressList.Clear();
            }
        }

        private MailMessage CreateMessage(EmailData data)
        {
            data.FromAddress = ConfigurationManager.AppSettings["SMTPLoginId"];
            var mail = new MailMessage
            {
                From = new MailAddress(data.FromAddress),
                IsBodyHtml = data.IsHtml,
                Body = data.Body,
                Subject = data.Subject
            };
            mail.To.Add(string.Join(",", data.ToAddressList));
            if (data.CCAddressList != null && data.CCAddressList.Any())
                mail.CC.Add(string.Join(",", data.CCAddressList));
            if (data.BCCAddressList != null && data.BCCAddressList.Any())
                mail.Bcc.Add(string.Join(",", data.BCCAddressList));
            return mail;
        }


        private void SendEmail(MailMessage msg)
        {
            var host = ConfigurationManager.AppSettings["SMTPHost"];
            var port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
            using (var client = new SmtpClient(host, port))
            {
                var id = ConfigurationManager.AppSettings["SMTPLoginId"];
                var pwd = ConfigurationManager.AppSettings["SMTPLoginPwd"];
                client.Credentials = new NetworkCredential(id, pwd);
                client.EnableSsl = true;
                client.Send(msg);
            }
        }
    }
}
