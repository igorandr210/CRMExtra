using Application.Authorization.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProfilesData.Queries
{
    public record GetAllUsersQuery() : IRequest<List<ProfileInfoDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ProfileInfoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProfileInfoDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.ProfileData.ToListAsync();
            var mappedResult = _mapper.Map<List<ProfileInfoDto>>(users);

            return mappedResult;
        }
    }
}
