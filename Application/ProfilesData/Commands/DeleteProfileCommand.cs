using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProfilesData.Commands
{
    public record DeleteProfileCommand(Guid UserId) : IRequest<Unit>;

    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public DeleteProfileCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _context.ProfileData.FirstOrDefaultAsync(x => x.Id == request.UserId);

            await _identityService.DeleteUser(userToDelete.Email);

            _context.ProfileData.Remove(userToDelete);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
