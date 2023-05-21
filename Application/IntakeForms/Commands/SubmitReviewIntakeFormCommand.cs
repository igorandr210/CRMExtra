using System;
using System.Threading;
using System.Threading.Tasks;
using Application.IntakeForms.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.IntakeForms.Commands
{
    public record SubmitReviewIntakeFormCommand(Guid UserId): IRequest<IntakeFormResponseDto>;

    public class SubmitReviewIntakeFormHandler: IRequestHandler<SubmitReviewIntakeFormCommand, IntakeFormResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IIntakeFormRepository _intakeFormRepository;

        public SubmitReviewIntakeFormHandler(IMapper mapper, IIntakeFormRepository intakeFormRepository)
        {
            _mapper = mapper;
            _intakeFormRepository = intakeFormRepository;
        }
        
        public async Task<IntakeFormResponseDto> Handle(SubmitReviewIntakeFormCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _intakeFormRepository.SubmitReviewFormAsync(request.UserId, cancellationToken);
            var mappedResult = _mapper.Map<IntakeFormResponseDto>(entity);
            
            return mappedResult;
        }
    }
}