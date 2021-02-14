﻿using System.Threading.Tasks;

namespace Core5Ng.Core.EF.Core
{
    public interface IFunctionalSvc
    {
        Task CreateDefaultAdminUser();
        Task CreateDefaultUser();
        //Task SendEmailByGmailAsync(string fromEmail, string fromFullName, string subject,
        //   string messageBody, string toEmail, string toFullName, string smtpUser, string smtpPassword,
        //   string smtpHost, int smtpPort, bool smtpSSL);
        //Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject,
        //    string message, string email);
    }
}
