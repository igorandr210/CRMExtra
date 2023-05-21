using Application.Documents.DTOs;
using Application.IntakeForms.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IntakeForms.Commands
{
    public record ExportExcelFileCommand(Guid Userid) : IRequest<DownloadFileDto>;
    public class ExportExcelFileCommandHandler : IRequestHandler<ExportExcelFileCommand, DownloadFileDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IFileExportService<IntakeFormExcelDto> _fileService;

        public ExportExcelFileCommandHandler(IMapper mapper, IApplicationDbContext context, IFileExportService<IntakeFormExcelDto> fileService)
        {
            _mapper = mapper;
            _context = context;
            _fileService = fileService;
        }

        public async Task<DownloadFileDto> Handle(ExportExcelFileCommand request, CancellationToken cancellationToken)
        {
            var intakeForm = await _context.IntakeForms
                .Include(x => x.User)
                .ThenInclude(x => x.StateInfo)
                .Include(x=>x.Spouse)
                .Include(x => x.StormInfo)
                .Include(x => x.InsuranceCompanyInfo)
                .Include(x=>x.InsuranceAgency)
                .Include(x => x.TypeOfOccupationInfo)
                .Include(x => x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x=>x.TypeOfLossInfo)
                .Include(x=>x.Mortgage)
                .Include(x=>x.Claim)
                .FirstOrDefaultAsync(x => x.UserId == request.Userid);

            var mappedIntakeForm = _mapper.Map<IntakeFormExcelDto>(intakeForm);

            return _fileService.Export(mappedIntakeForm, $"intakeForm_{intakeForm.User.IdForYear}");
        }
    }
}
