using Application.IntakeForms.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IntakeForms.Commands
{
    public record UnSubmitFormCommand(UnSubmitFormDto Data, Guid UserId) : IRequest<bool>;
    public class UnSubmitFormCommandHandler : IRequestHandler<UnSubmitFormCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnSubmitFormCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UnSubmitFormCommand request, CancellationToken cancellationToken)
        {
            var intakeForm = await _context.IntakeForms.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == request.UserId);

            intakeForm.IsFilled = false;
            intakeForm.IsDraft = true;

            var message = $"Hey,<br>You were asked to update the form: {request.Data.EmailMessage}.<br>Please click the <a href='https://extracrm.pp.ua/intake-form'>link</a> to proceed.";

            intakeForm.AddDomainEvent(new EnvelopeCreated
               (
               "crmdevelopment2022@gmail.com", intakeForm.User.Email, message, $"Please update your intake form #{intakeForm.User.IdForYear}"
               )
           );

            _context.IntakeForms.Update(intakeForm);

            await _context.SaveChangesAsync();

            return intakeForm.IsFilled;
        }
    }
}
