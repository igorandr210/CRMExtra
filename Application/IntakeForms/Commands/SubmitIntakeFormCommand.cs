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
    public record SubmitIntakeFormCommand(IntakeFormRequestDto Data, Guid UserId) : IRequest<IntakeFormResponseDto>;

    public class SubmitIntakeFormHandler : IRequestHandler<SubmitIntakeFormCommand, IntakeFormResponseDto>
    {
        private readonly IIntakeFormRepository _intakeFormRepository;
        private readonly IMapper _mapper;

        public SubmitIntakeFormHandler(IIntakeFormRepository intakeFormRepository, IMapper mapper)
        {
            _intakeFormRepository = intakeFormRepository;
            _mapper = mapper;
        }

        public async Task<IntakeFormResponseDto> Handle(SubmitIntakeFormCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var mappedEntity = _mapper.Map<IntakeForm>(request.Data);
            
            mappedEntity.UserId = userId;
            mappedEntity.IsDraft = false;
            mappedEntity.IsConfirmed = false;
            mappedEntity.IsFilled = true;

            var entity = await _intakeFormRepository.UpdateFormAsync(mappedEntity, cancellationToken);
            var mappedResult = _mapper.Map<IntakeFormResponseDto>(entity);
            
            return mappedResult;
        }
    }
}