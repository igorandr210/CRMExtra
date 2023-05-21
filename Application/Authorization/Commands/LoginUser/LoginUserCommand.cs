using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Application.Authorization.DTOs;

namespace Application.Authorization.Commands.LoginUser
{
    public record LoginUserCommand(LoginDto Data) : IRequest<LoginResponseDto>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationDbContext _context;

        public LoginUserCommandHandler(IIdentityService identityService, IApplicationDbContext context)
        {
            _identityService = identityService;
            _context = context;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var identityLoginResult = await _identityService.LoginUser(request.Data);
            var user = await _context.ProfileData.FirstOrDefaultAsync(x => x.Email == request.Data.Email, cancellationToken);

            identityLoginResult.UserId = user.Id;

            return identityLoginResult;
        }
    }
}
