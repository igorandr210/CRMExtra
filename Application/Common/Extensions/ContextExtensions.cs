using System.Linq;
using Application.Common.Enums;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Common.Extensions
{
    public static class ContextExtensions
    {
        public static IQueryable<BaseDropDownEntity<string>> GetDropdownSet(this IApplicationDbContext context, DropDownType type)
        {
            IQueryable<BaseDropDownEntity<string>> entitiesQueryable = type switch
            {
                DropDownType.State => context.Set<State>(),
                _ => throw new NotFoundException(type.ToString()),
            };
            
            return entitiesQueryable;
        }
    }
}