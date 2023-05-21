using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Common;
using Quartz.Util;

namespace Infrastructure.Persistence.Repositories.Abstractions
{
    public abstract class PaginatedRepository<TEntity, TPaginationInfo> : BaseRepository<TEntity>,
        IPaginatedRepository<TEntity, TPaginationInfo>
        where TPaginationInfo : PaginationInfoDto
        where TEntity : BaseEntity
    {
        protected virtual Dictionary<string, string> SortColumnMapping { get; init; } =
            new(StringComparer.OrdinalIgnoreCase);

        protected PaginatedRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected virtual IOrderedQueryable<TEntity> GetSortedQuery(IQueryable<TEntity> dataSource,
            PaginationInfoDto data)
        {
            if (SortColumnMapping.TryGetValue(data.SortByColumn, out var mappedColumnName))
            {
                data.SortByColumn = mappedColumnName;
            }

            var parameter = Expression.Parameter(typeof(TEntity));

            var memberExpression =
                parameter.Property(data.SortByColumn.IsNullOrWhiteSpace() ? "id" : data.SortByColumn);

            var orderBy = Expression.Lambda<Func<TEntity, object>>(
                Expression.Convert(memberExpression, typeof(object)), parameter);

            return data.AscSort ? dataSource.OrderBy(orderBy) : dataSource.OrderByDescending(orderBy);
        }

        protected IQueryable<TEntity> GetPaginatedQuery(IQueryable<TEntity> dataSource, PaginationInfoDto pagination)
        {
            return GetSortedQuery(dataSource, pagination)
                .Skip((pagination.PageNumber - 1) * pagination.AmountPerPage)
                .Take(pagination.AmountPerPage);
        }

        public abstract Task<PaginatedDataDto<TEntity>> GetPaginatedDataAsync(TPaginationInfo pagination);
    }
}