using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Application.Common.DTOs;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly AmazonSimpleEmailServiceClient _client;

        public EmailService(AmazonSimpleEmailServiceClient client)
        {
            _client = client;
        }

        public async Task<bool> IsVerified(string email)
        {
            var response = await _client.GetIdentityMailFromDomainAttributesAsync(new GetIdentityMailFromDomainAttributesRequest { Identities = new List<string>() { email } });

            return response.ResponseMetadata.Metadata.Count > 0;
        }

        public async Task SendEmail(SendEmailDto emailInfo)
        {
            var emailRequest = new SendEmailRequest()
            {
                Source = emailInfo.From,
                Destination = new Destination(),
                Message = new Message()
            };

            emailRequest.Destination.ToAddresses.Add(emailInfo.Destination);
            emailRequest.Message.Subject = new Content(emailInfo.Subject);
            emailRequest.Message.Body = new Body
            {
                Html = new Content
                {
                    Charset = "UTF-8",
                    Data = emailInfo.Message
                }
            };

            await _client.SendEmailAsync(emailRequest);
        }

        public async Task<bool> Verify(string email)
        {
            var result = await _client.VerifyEmailIdentityAsync(new VerifyEmailIdentityRequest { EmailAddress = email });

            return result.ResponseMetadata.Metadata.Count > 0;
        }
    }
}