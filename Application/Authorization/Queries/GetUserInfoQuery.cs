using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authorization.Queries
{
    public record GetUserInfoQuery : IRequest<UserInfoDto>;

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IApplicationDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }
        
        public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var roles = _userService.Roles;
            var userData = await _context.ProfileData.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            var mappedResult = _mapper.Map<UserInfoDto>(userData);

            mappedResult.Roles = roles;

            return mappedResult;
        }
    }
}