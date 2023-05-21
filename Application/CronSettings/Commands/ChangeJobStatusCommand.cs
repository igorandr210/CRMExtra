using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CronSettings.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CronSettings.Commands
{
    public record ChangeJobStatusCommand(ChangeJobDto JobModel) : IRequest<JobCronSettingDto>;

    public class ChangeJobStatusHandler : IRequestHandler<ChangeJobStatusCommand, JobCronSettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChangeJobStatusHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<JobCronSettingDto> Handle(ChangeJobStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var setting = await _context.JobCronSettings.FirstOrDefaultAsync(x => x.JobType == request.JobModel.JobType,
                    cancellationToken);
                setting.Status = request.JobModel.Status;

                if (!string.IsNullOrEmpty(request.JobModel.CronString))
                {
                    setting.CronSettingString = request.JobModel.CronString;
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<JobCronSettingDto>(setting);
                return mappedResult;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}