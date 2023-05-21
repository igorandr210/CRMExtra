using System;
using System.Threading;
using System.Threading.Tasks;
using Application.IntakeForms.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.IntakeForms.Commands
{
    public record SaveDraftIntakeFormCommand(IntakeFormRequestDto Data, Guid UserId): IRequest<IntakeFormResponseDto>;

    public class SaveDraftIntakeFormHandler: IRequestHandler<SaveDraftIntakeFormCommand, IntakeFormResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IIntakeFormRepository _intakeFormRepository;
        private readonly IApplicationDbContext _context;

        public SaveDraftIntakeFormHandler(IMapper mapper, IIntakeFormRepository intakeFormRepository, IApplicationDbContext context)
        {
            _mapper = mapper;
            _intakeFormRepository = intakeFormRepository;
            _context = context;
        }
        
        public async Task<IntakeFormResponseDto> Handle(SaveDraftIntakeFormCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var mappedEntity = _mapper.Map<IntakeForm>(request.Data);
            var existedIntakeForm = await _context.IntakeForms.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == request.UserId);
            var isSubmittedForm = existedIntakeForm?.IsFilled ?? false;

            mappedEntity.UserId = userId;
            mappedEntity.IsDraft = true;
            mappedEntity.IsConfirmed = false;
            mappedEntity.IsFilled = false;

            if (isSubmittedForm)
            {
                mappedEntity.IsDraft = false;
                mappedEntity.IsFilled = true;
            }

            var entity = await _intakeFormRepository.UpdateFormAsync(mappedEntity, cancellationToken);
            var mappedResult = _mapper.Map<IntakeFormResponseDto>(entity);
            
            return mappedResult;
        }
    }
}