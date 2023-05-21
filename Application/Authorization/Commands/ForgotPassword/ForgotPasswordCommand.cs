using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Authorization.DTOs;

namespace Application.Authorization.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(ForgotPasswordDto Data): IRequest<Unit>;

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand,Unit>
    {
        private readonly IIdentityService _identityService;

        public ForgotPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            await _identityService.ForgotPassword(request.Data.Email);
            return Unit.Value;
        }
    }
}
