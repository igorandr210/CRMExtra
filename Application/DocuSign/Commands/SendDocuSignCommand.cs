using Application.Authorization.DTOs;
using Application.Common.Enums;
using Application.Common.Factories;
using Application.DocuSign.DTOs;
using Application.Interfaces;
using Application.Properties;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Applicatio.DocuSign.Commands
{
    public record SendDocuSignCommand(Guid userId) : IRequest<GetEnvelopeDto>;
    public class SendDocuSignCommandHandler : IRequestHandler<SendDocuSignCommand, GetEnvelopeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDocuSignService _service;

        public SendDocuSignCommandHandler(IApplicationDbContext context, IMapper mapper, IDocuSignService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<GetEnvelopeDto> Handle(SendDocuSignCommand request, CancellationToken cancellationToken)
        {
            var intakeForm = await _context.IntakeForms.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == request.userId);
            var docuSignResponse = await _service.SendEnvelopeForEmbeddedSigning(intakeForm.User.Email, intakeForm.User.InsuredName);
            var envelope = new Envelope { Id = docuSignResponse.EnvelopeId, IntakeFormId = intakeForm.Id, CustomersEnvelopeLink = docuSignResponse.ViewUrl.Url };
            var mappedResult = _mapper.Map<GetEnvelopeDto>(envelope);
            var message = string.Format(Resources.VERIFY_EMAIL_HTML, docuSignResponse.ViewUrl.Url);

            envelope.AddDomainEvent(new EnvelopeCreated("crmdevelopment2022@gmail.com", intakeForm.User.Email, message, "Please sign this document"));

            await _context.Envelopes.AddAsync(envelope);
            await _context.SaveChangesAsync();

            return mappedResult;
        }
    }

}
