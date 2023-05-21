using System;
using System.Threading;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.AssignmentTasks.Queries
{
    public record GetTaskByIdQuery(Guid TaskId) : IRequest<GetTaskByIdResponseDto>;

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<AssignmentTask> _repository;

        public GetTaskByIdQueryHandler(IMapper mapper, IBaseRepository<AssignmentTask> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetTaskByIdResponseDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                await _repository.GetByIdAsync(request.TaskId);
            
            if (result is null)
            {
                throw new NotFoundException(request.TaskId.ToString());
            }

            var mappedResult = _mapper.Map<GetTaskByIdResponseDto>(result);

            return mappedResult;
        }

    }
}