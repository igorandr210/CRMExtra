using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Documents.DTOs;
using Application.Interfaces;
using Application.ProfilesData.Dto;
using AutoMapper;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProfilesData.Commands
{
    public record ExportExcelFileQuery() : IRequest<DownloadFileDto>;

    public class ExportExcelFileQueryHandler : IRequestHandler<ExportExcelFileQuery, DownloadFileDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileExportService<FileExportProfileDto> _fileService;
        public ExportExcelFileQueryHandler(IApplicationDbContext context, IMapper mapper, IFileExportService<FileExportProfileDto> fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<DownloadFileDto> Handle(ExportExcelFileQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _context.ProfileData
                .Include(x => x.IntakeForm)
                .ThenInclude(x => x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x=>x.TypeOfLossInfo)
                .Where(x => x.Roles.Contains(Role.Customer))
                .ToListAsync(cancellationToken);

            var mappedProfiles = _mapper.Map<IEnumerable<FileExportProfileDto>>(profiles);

            return _fileService.Export(mappedProfiles, "Profiles");
        }
    }
}
