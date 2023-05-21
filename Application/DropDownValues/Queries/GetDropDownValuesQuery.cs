using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.DropDownValues.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DropDownValues.Queries
{
    public record GetDropDownValuesQuery(DropDownType Type) : IRequest<IEnumerable<DropDownValue<string>>>;

    public class GetDropDownValuesQueryHandler : IRequestHandler<GetDropDownValuesQuery, IEnumerable<DropDownValue<string>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDropDownValuesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DropDownValue<string>>> Handle(GetDropDownValuesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<BaseDropDownEntity<string>> entitiesQueryable = request.Type switch
            {
                DropDownType.State => _context.Set<State>(),
                DropDownType.TypeOfLoss => _context.Set<TypeOfLoss>(),
                DropDownType.InsuranceCompany => _context.Set<InsuranceCompany>(),
                DropDownType.TypeOfOccupation => _context.Set<TypeOfOccupation>(),
                DropDownType.StormName => _context.Set<Storm>(),
                DropDownType.SelfOccupation => _context.Set<SelfOccupation>(),
                DropDownType.TaskType => _context.Set<TaskType>(),
                DropDownType.RoofDamage => _context.Set<RoofDamage>(),
                _ => throw new NotFoundException(request.Type.ToString()),
            };

            var entities = await entitiesQueryable.ToListAsync(cancellationToken);
            var mappedEntities = _mapper.Map<List<DropDownValue<string>>>(entities);

            mappedEntities.Insert(0,new() {Id = null, Value = ""});
            
            return mappedEntities;
        }
    }
}