using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using Application.IntakeForms.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.IntakeForms.Queries
{
    public record GetIntakeFormByUserIdQuery(Guid Id) : IRequest<IntakeFormResponseDto>;

    public class GetIntakeFormQueryHandler : IRequestHandler<GetIntakeFormByUserIdQuery, IntakeFormResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetIntakeFormQueryHandler(IApplicationDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }
        
        public async Task<IntakeFormResponseDto> Handle(GetIntakeFormByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.IntakeForms
                .Include(x => x.Claim)
                .Include(x => x.Mortgage)
                .Include(x => x.InsuranceAgency)
                .Include(x => x.Documents)
                .Include(x => x.Spouse)
                .Include(x => x.User)
                .Include(x => x.ClaimChecks)
                .ThenInclude(x => x.Document)
                .Include(x=>x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x=>x.TypeOfLossInfo)
                .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);

            var mappedResult = _mapper.Map<IntakeFormResponseDto>(result);
            
            return mappedResult;
        }
    }
}