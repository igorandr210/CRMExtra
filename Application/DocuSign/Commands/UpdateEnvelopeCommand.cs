using Application.Common.Factories;
using Application.DocuSign.DTOs;
using Application.Interfaces;
using AutoMapper;
using DocuSign.eSign.Model;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DocuSign.Commands
{
    public record UpdateEnvelopeCommand(EnvelopeDto Data) : IRequest<GetEnvelopeDto>;

    public class UpdateEnvelopeCommandHandler : IRequestHandler<UpdateEnvelopeCommand, GetEnvelopeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _documentsStorageService;
        private readonly IDocuSignService _service;

        public UpdateEnvelopeCommandHandler(BucketServiceFactory bucketServiceFactory, IApplicationDbContext context, IMapper mapper, IDocuSignService service)
        {
            _context = context;
            _mapper = mapper;
            _documentsStorageService = bucketServiceFactory.Invoke(Common.Enums.BucketType.EnvelopesCRM);
            _service = service;
        }

        public async Task<GetEnvelopeDto> Handle(UpdateEnvelopeCommand request, CancellationToken cancellationToken)
        {
            var envelopeId = Guid.Parse(request.Data.Data.EnvelopeId);
            var envelope = await _context.Envelopes.Include(x => x.IntakeForm).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == envelopeId);

            envelope.IsAdminSign = request.Data.Data.EnvelopeSummary.Recipients.Signers.FirstOrDefault(x => x.RoleName == "Admin").Status == "completed";
            envelope.IsCustomerSign = request.Data.Data.EnvelopeSummary.Recipients.Signers.FirstOrDefault(x => x.RoleName == "Customer").Status == "completed";

            var mappedResult = _mapper.Map<GetEnvelopeDto>(envelope);

            if (envelope.IsAdminSign && envelope.IsCustomerSign)
            {
                var indexToSlice = envelope.IntakeForm.User.Email.IndexOf('+') != -1 ? envelope.IntakeForm.User.Email.IndexOf('+') : envelope.IntakeForm.User.Email.IndexOf('@');
                var userEmail = envelope.IntakeForm.User.Email.Remove(indexToSlice);
                var key = $"{userEmail}/{envelopeId}";
                var file = await _service.GetEnvelopeFile(envelope.Id.ToString());

                await _documentsStorageService.UploadFile(key, file);
            }

            _context.Envelopes.Update(envelope);

            await _context.SaveChangesAsync();

            return mappedResult;
        }
    }
}
