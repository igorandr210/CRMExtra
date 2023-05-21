using Application.DocuSign.DTOs;
using Application.Interfaces;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Infrastructure.Configurations;
using Infrastructure.Properties;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DocuSignService : IDocuSignService
    {
        private readonly DocuSignConfiguration _configuration;

        private string AccessToken { get; set; }
        private DateTime? ExpireIn { get; set; }
        private string AccountId { get; set; }

        public DocuSignService(IOptions<DocuSignConfiguration> docuSignConfiguration)
        {
            _configuration = docuSignConfiguration?.Value ?? throw new Exception($"{nameof(docuSignConfiguration)} injection error");
        }

        public Task<string> AuthenticateWithJwt()
        {
            if (IsJwtAuthenticated())
            {
                return Task.FromResult(AccessToken);
            }

             var scopes = new List<string>
            {
                "signature",
                "impersonation",
            };
            var apiClient = new ApiClient();
            var key = Resources.CRMDevelopmentKeyPair;
            var jwtResult = apiClient.RequestJWTUserToken(_configuration.IntegrationKey, _configuration.DocuSignUserId,
                _configuration.AuthServer, key, 1, scopes);

            if (jwtResult is not { expires_in: { } })
            {
                throw new Exception("Error while JWT generating");
            }

            ExpireIn = DateTime.Now.AddSeconds(jwtResult.expires_in.Value);

            return Task.FromResult(jwtResult.access_token);
        }

        public async Task<string> GetAccountId()
        {
            if (!string.IsNullOrWhiteSpace(AccountId))
            {
                return AccountId;
            }

            var apiClient = new ApiClient();

            apiClient.SetOAuthBasePath(_configuration.AuthServer);

            var accessToken = await AuthenticateWithJwt();
            var userInfo = apiClient.GetUserInfo(accessToken);

            if (userInfo == null || userInfo.Accounts.Count < 1)
            {
                throw new Exception("Error while account_id generating");
            }

            var accountId = userInfo.Accounts.First().AccountId;

            return accountId;
        }

        public async Task<DocuSignDto> SendEnvelopeForEmbeddedSigning(string clientEmail,string clientName)
        {
            var jwtToken = await AuthenticateWithJwt();
            var envelope = MakeEnvelope(clientEmail, clientName, _configuration.IntegrationKey, _configuration.TemplateId );
            var notification = new Notification();

            notification.UseAccountDefaults = "false";
            notification.Reminders = new Reminders
            {
                ReminderEnabled = "true",
                ReminderDelay = "3", 
                ReminderFrequency = "2"
            };
            notification.Expirations = new Expirations
            {
                ExpireEnabled = "true",
                ExpireAfter = "30",
                ExpireWarn = "2" 
            };
            envelope.Notification = notification;

            var accountId = await GetAccountId();
            var apiClient = new ApiClient(_configuration.BasePath);

            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + jwtToken);

            var envelopesApi = new EnvelopesApi(apiClient);
            var envelopeCreateResult = await envelopesApi.CreateEnvelopeAsync(accountId, envelope);
            var envelopeId = envelopeCreateResult.EnvelopeId;
            var viewRequest = MakeRecipientViewRequest(clientEmail, clientName, _configuration.RedirectUrl, _configuration.IntegrationKey);
            

            var resultCreateRecipient = await envelopesApi.CreateRecipientViewAsync(accountId, envelopeId, viewRequest);
            var result = new DocuSignDto { EnvelopeId = Guid.Parse(envelopeId), ViewUrl = resultCreateRecipient };

            return result;
        }

        private static RecipientViewRequest MakeRecipientViewRequest(string signerEmail, string signerName, string returnUrl, string signerClientId)
        {
            var viewRequest = new RecipientViewRequest
            {
                ReturnUrl = returnUrl,
                AuthenticationMethod = "none",
                Email = signerEmail,
                UserName = signerName,
                ClientUserId = signerClientId
            };

            return viewRequest;
        }


        public async Task<Envelope> GetEnvelope(string envelopeId)
        {
            var jwtToken = await AuthenticateWithJwt();
            var apiClient = new ApiClient(_configuration.BasePath);

            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + jwtToken);

            var envelopesApi = new EnvelopesApi(apiClient);
            var accountId = await GetAccountId();
            var envelope = await envelopesApi.GetEnvelopeAsync(accountId, envelopeId);

            return envelope;
        }

        public async Task<EnvelopeFormData> GetDataFromEnvelope(string envelopeId)
        {
            var jwtToken = await AuthenticateWithJwt();
            var apiClient = new ApiClient(_configuration.BasePath);

            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + jwtToken);

            var envelopesApi = new EnvelopesApi(apiClient);
            var accountId = await GetAccountId();
            var formData = await envelopesApi.GetFormDataAsync(accountId, envelopeId);

            return formData;
        }

        public async Task<Signer> GetAdvisorByEnvelope(Envelope envelope)
        {
            var jwtToken = await AuthenticateWithJwt();
            var apiClient = new ApiClient(_configuration.BasePath);

            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + jwtToken);

            var envelopesApi = new EnvelopesApi(apiClient);
            var accountId = await GetAccountId();
            var recipients = await envelopesApi.ListRecipientsAsync(accountId, envelope.EnvelopeId);
            var signer = recipients.Signers.FirstOrDefault();

            return signer;
        }

        public async Task<Stream> GetEnvelopeFile(string envelopeId)
        {
            var jwtToken = await AuthenticateWithJwt();
            var apiClient = new ApiClient(Environment.CurrentDirectory + _configuration.BasePath);

            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + jwtToken);

            var envelopesApi = new EnvelopesApi(apiClient);
            var accountId = await GetAccountId();
            var documentStream = await envelopesApi.GetDocumentAsync(accountId, envelopeId, "combined");

            return documentStream;
        }

        private bool IsJwtAuthenticated(int bufferMin = 60)
        {
            return ExpireIn != null && AccessToken != null && (DateTime.Now.Subtract(TimeSpan.FromMinutes(bufferMin)) < ExpireIn.Value);
        }

        private static EnvelopeDefinition MakeEnvelope(string signerEmail, string signerName, string signerClientId, string templateId)
        {
            var envelopeDefinition = new EnvelopeDefinition
            {
                EmailSubject = "Please sign this document",
                TemplateId = templateId,
                TemplateRoles = new List<TemplateRole>()
                {
                    new()
                    {
                        Email = signerEmail,
                        Name = signerName,
                        RoleName = "Customer",
                        ClientUserId = signerClientId,
                    }
                },
                Recipients = new Recipients(),
                Status = "sent",
            };

            return envelopeDefinition;
        }
    }
}
