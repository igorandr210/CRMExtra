using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Extensions;
using Application.DropDownValues.DTOs;
using Application.DropDownValues.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.DropDownValues.Commands
{
    public record CreateDropDownValueCommand(CreateDropdownRequestDto Data) : IRequest<DropDownValue<string>>;

    public class CreateDropDownValueHandler : IRequestHandler<CreateDropDownValueCommand, DropDownValue<string>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDropDownValueHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<DropDownValue<string>> Handle(CreateDropDownValueCommand request, CancellationToken cancellationToken)
        {
            BaseDropDownEntity<string> result;
            switch (request.Data.Type)
            {
                case DropDownType.State:
                    var addResult = await _context
                        .States
                        .AddAsync(new State { Value = request.Data.DropDownValue.Value }, cancellationToken);
                    result = addResult.Entity;
                    break;
                case DropDownType.InsuranceCompany:
                    var addICResult = await _context
                        .InsuranceCompanies
                        .AddAsync(new InsuranceCompany { Value = request.Data.DropDownValue.Value }, cancellationToken);
                    result = addICResult.Entity;
                    break;
                default:
                    throw new UnhandledException($"Type {request.Data.Type} unhandled.");
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DropDownValue<string>{ Id = result.Id, Value = result.Value};
        }
    }
}