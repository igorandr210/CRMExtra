using Application.Authorization.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.ProfilesData.Dto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProfilesData.Queries
{
    public record GetUserByIdQuery(Guid userId) : IRequest<UserByIdDto>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserByIdDto>
    {
        private readonly IBaseRepository<ProfileData> _repository;
        private readonly IMapper _mapper;
        
        public GetUserByIdQueryHandler(IBaseRepository<ProfileData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.userId);
            //var user = await _repository.ProfileData.Include(x => x.StateInfo).FirstOrDefaultAsync(x => x.Id == request.userId);
          
            var mappedResult = _mapper.Map<UserByIdDto>(user);

            return mappedResult;
        }
    }
}
