using Application.DocuSign.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DocuSign.Queries
{
    public record GetEnvelopeQuery(Guid UserId) : IRequest<GetEnvelopeDto>;

    public class GetEnvelopeQueryHandler : IRequestHandler<GetEnvelopeQuery, GetEnvelopeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEnvelopeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetEnvelopeDto> Handle(GetEnvelopeQuery request, CancellationToken cancellationToken)
        {
            var envelope = await _context.Envelopes.Include(x => x.IntakeForm).FirstOrDefaultAsync(x => x.IntakeForm.UserId == request.UserId);
            var mappedResult = _mapper.Map<GetEnvelopeDto>(envelope);

            return mappedResult;
        }
    }
}
