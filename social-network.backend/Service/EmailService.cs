﻿using social_network.backend.Interfaces;
using System.Net;
using System.Net.Mail;
namespace social_network.backend.Service
{
    public class EmailService : IEmailService
    {

        public async Task SendEmailAsync(string email, string subject, string code)
        {
            using var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("sinkov407@gmail.com", "lpmc nxdd euzn gavd");
            smtp.EnableSsl = true;
            var mail = new MailMessage("sinkov407@gmail.com", email, subject, code);
            await smtp.SendMailAsync(mail);

        }

        public async Task SendTwoFactorCodeAsync(string email, string code)
        {
            var subject = "Код подтверждения для двухфакторной аутентификации";
            Random random = new Random();
            code = random.Next(100000, 999999).ToString();
            await SendEmailAsync(email, subject, code);
        }
    }
}
