using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Authorization.Commands.RegisterUser
{
    public record RegisterUserCommand(SignUpDto Data) : IRequest<Guid?>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public RegisterUserCommandHandler(IApplicationDbContext context, IIdentityService identityService,
            IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Guid?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var identityCreateResult = await _identityService.CreateUser(request.Data);

            if (identityCreateResult.IsSuccess)
            {
                var profileData = _mapper.Map<ProfileData>(request.Data);

                profileData.Id = identityCreateResult.UserId;
                profileData.Roles = new Role[]
                {
                    identityCreateResult.Role
                };

                var data = await _context.ProfileData.AddAsync(profileData, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                if (await _emailService.IsVerified(data.Entity.Email) == false)
                {
                    await _emailService.Verify(data.Entity.Email);
                }

                return data.Entity.Id;
            }

            return null;
        }
    }
}