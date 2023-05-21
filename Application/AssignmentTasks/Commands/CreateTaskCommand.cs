using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Common.Extensions;
using Application.Common.Factories;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.AssignmentTasks.Commands
{
    public record CreateTaskCommand(CreateTaskRequestDto Data) : IRequest<CreateTaskResponseDto>;

    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<AssignmentTask> _repository;

        public CreateTaskHandler(IMapper mapper, IBaseRepository<AssignmentTask> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreateTaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var mappedEntity = _mapper.Map<AssignmentTask>(request.Data);
            var result = await _repository.InsertAsync(mappedEntity);
            var mappedResult = _mapper.Map<CreateTaskResponseDto>(result);

            await _repository.SaveChangesAsync();

            return mappedResult;
        }
    }
}
