using System.Threading;
using System.Threading.Tasks;
using Application.CronSettings.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CronSettings.Queries
{
    public record GetCronSettingsQuery(JobType JobType) : IRequest<JobCronSettingDto>;
    
    public class GetCronSettingsHandler
    {
        public class GetCronSettingHandler : IRequestHandler<GetCronSettingsQuery, JobCronSettingDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCronSettingHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JobCronSettingDto> Handle(GetCronSettingsQuery request, CancellationToken cancellationToken)
            {
                var setting = await _context.JobCronSettings.AsNoTracking().FirstOrDefaultAsync(x => x.JobType == request.JobType,
                    cancellationToken);
                
                return _mapper.Map<JobCronSettingDto>(setting);
            }
        }
    }
}