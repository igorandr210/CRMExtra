using Application.Common.DTOs;
using Application.Interfaces;
using Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class EmailEventHandler : INotificationHandler<EnvelopeCreated>
    {
        private readonly IEmailService _emailService;

        public EmailEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(EnvelopeCreated envelopeEmailInfo, CancellationToken cancellationToken)
        {

            await _emailService.SendEmail(new SendEmailDto
            {
                From = envelopeEmailInfo.From,
                Destination = envelopeEmailInfo.Destination,
                Subject = envelopeEmailInfo.Subject,
                Message = envelopeEmailInfo.Message
            });
        }
    }
}
