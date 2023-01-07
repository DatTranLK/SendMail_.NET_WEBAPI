using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SimpleEmailApp.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace SimpleEmailApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMail(EmailDto request)
        {
            /*var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);*/
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_configuration.GetSection("EmailUserName").Value);
            message.Subject = request.Subject;
            message.To.Add(new MailAddress(request.To));
            var list = new List<string> { "Hello", "meo" };
            message.Body = "Hello nè: ";
            foreach (var item in list)
            {
                message.Body += item + ", ";
            }
            /*message.Body = request.Body;*/
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
