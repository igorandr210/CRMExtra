using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Authorization.DTOs;
using Application.Common.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.ProfilesData.Dto;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ProfilesData.Queries
{
    public record GetProfileListQuery(PaginationInfoProfileDto Data) : IRequest<PaginatedDataDto<ProfileInfoDto>>;
    
    public class GetProfileListQueryHandler: IRequestHandler<GetProfileListQuery, PaginatedDataDto<ProfileInfoDto>>
    {
        private readonly IPaginatedRepository<ProfileData, PaginationInfoProfileDto> _paginatedRepository;
        private readonly IMapper _mapper;

        public GetProfileListQueryHandler(IPaginatedRepository<ProfileData, PaginationInfoProfileDto> paginatedRepository, IMapper mapper)
        {
            _paginatedRepository = paginatedRepository;
            _mapper = mapper;
        }
        
        public async Task<PaginatedDataDto<ProfileInfoDto>> Handle(GetProfileListQuery request, CancellationToken cancellationToken)
        {
            var users = await _paginatedRepository.GetPaginatedDataAsync(request.Data);

            var mappedResult = _mapper.Map<PaginatedDataDto<ProfileInfoDto>>(users);


            return mappedResult;
        }
    }
}