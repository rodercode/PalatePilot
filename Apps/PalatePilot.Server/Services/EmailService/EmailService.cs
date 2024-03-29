using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PalatePilot.Server.Configs;
using PalatePilot.Server.Exceptions;
using PalatePilot.Server.Models.Dto;
using Serilog;
namespace PalatePilot.Server.Services.EmailService
{
    public class EmailService: IEmailService 
    {
        private readonly EmailConfig _emailConfig;
        public EmailService(IOptions<EmailConfig> emailConfigOptions)
        {
            _emailConfig = emailConfigOptions.Value;
        }

        public async Task SendEmailAsync(EmailDto request)
        {
            var emailMessage = CreateEmailMessage(request);
            await SendAsync(emailMessage);
        } 

        
        private async Task SendAsync(MimeMessage emailMessage)
        {
            try
            {
                using(var mailClient = new SmtpClient())
                {
                    // Connect to the email server
                    await mailClient.ConnectAsync(_emailConfig.Server, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
            
                    // Authenticate the email sender
                    await mailClient.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
            
                    // Send the email
                    await mailClient.SendAsync(emailMessage);

                    // Disconnect from the email server
                    await mailClient.DisconnectAsync(true);  
                }
            }

            catch(Exception)
            {
                Log.Error("Send Email Process: failed due to a configuration issue.");
                throw new InternalServerErrorException("We encountered an issue while processing your request. Please try again later");
            }
        }

        private MimeMessage CreateEmailMessage(EmailDto request)
        {
            // Initialize the email message
            var emailMessage = new MimeMessage();
            
            // Generate a email sender
            MailboxAddress sender = new MailboxAddress(_emailConfig.Username, _emailConfig.Email);
            emailMessage.From.Add(sender);
            
            // Generate a email receiver
            MailboxAddress receiver = new MailboxAddress(request.Username, request.Email);
            emailMessage.To.Add(receiver);

            // Generate a email subject
            emailMessage.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder {HtmlBody = request.Message,};

            // Generate a email message
            emailMessage.Body = bodyBuilder.ToMessageBody();
            
            return emailMessage;
        }
    }
}