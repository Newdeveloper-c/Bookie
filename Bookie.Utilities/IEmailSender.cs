﻿namespace Bookie.Utilities;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}