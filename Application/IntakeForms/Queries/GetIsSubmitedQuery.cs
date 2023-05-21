using Application.Authorization.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IntakeForms.Queries
{
    public record GetIsSubmitedQuery(Guid UserId) : IRequest<bool>;

    public class IsSubmitedCommandHandler : IRequestHandler<GetIsSubmitedQuery, bool>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public IsSubmitedCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> Handle(GetIsSubmitedQuery request, CancellationToken cancellationToken)
        {
            var intakeForm = await _context.IntakeForms.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            return intakeForm?.IsFilled ?? false;
        }
    }
}
