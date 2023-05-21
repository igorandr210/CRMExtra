using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CronSettings.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CronSettings.Commands
{
    public record CreateCronSettingCommand(JobCronSettingDto CronSetting) : IRequest<JobCronSettingDto>;

    public class CreateCronSettingHandler : IRequestHandler<CreateCronSettingCommand, JobCronSettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCronSettingHandler> _logger;

        public CreateCronSettingHandler(IApplicationDbContext context, 
            IMapper mapper,
            ILogger<CreateCronSettingHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobCronSettingDto> Handle(CreateCronSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedRequest = _mapper.Map<JobCronSetting>(request.CronSetting);
                await _context.JobCronSettings.AddAsync(mappedRequest, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                
                return request.CronSetting;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}