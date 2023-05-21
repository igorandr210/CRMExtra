using System;
using System.Threading;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.AssignmentTasks.Commands
{
    public record EditTaskCommand(EditTaskRequestDto Data, Guid TaskId) : IRequest<GetTaskByIdResponseDto>;

    public class EditTaskHandler : IRequestHandler<EditTaskCommand, GetTaskByIdResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<AssignmentTask> _repository;

        public EditTaskHandler(IMapper mapper, IBaseRepository<AssignmentTask> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetTaskByIdResponseDto> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.TaskId);

            if (entity is null)
            {
                throw new NotFoundException(request.TaskId.ToString());
            }

            _mapper.Map(request.Data, entity);

            _repository.Update(entity);
            entity = await _repository.GetByIdAsync(request.TaskId);
            var mappedResult = _mapper.Map<GetTaskByIdResponseDto>(entity);

            await _repository.SaveChangesAsync();

            return mappedResult;
        }
    }
}