using System.Threading;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Common.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.AssignmentTasks.Queries
{
    public record GetTasksQuery(PaginationInfoTaskDto Data) : IRequest<PaginatedDataDto<GetTasksResponseDto>>;

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, PaginatedDataDto<GetTasksResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPaginatedRepository<AssignmentTask, PaginationInfoTaskDto> _repository;

        public GetTasksQueryHandler(IMapper mapper, IPaginatedRepository<AssignmentTask, PaginationInfoTaskDto> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PaginatedDataDto<GetTasksResponseDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetPaginatedDataAsync(request.Data);
            var mappedResult = _mapper.Map<PaginatedDataDto<GetTasksResponseDto>>(result);

            return mappedResult;
        }

    }
}