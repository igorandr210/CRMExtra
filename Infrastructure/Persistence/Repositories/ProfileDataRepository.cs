using System.Collections.Generic;
using Application.Common.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using System;
using Infrastructure.Persistence.Repositories.Abstractions;
using Application.AssignmentTasks.DTOs;
using Application.ProfilesData.Dto;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfileDataRepository : PaginatedRepository<ProfileData, PaginationInfoProfileDto>
    {
        protected override Dictionary<string, string> SortColumnMapping =>
            new(StringComparer.OrdinalIgnoreCase)
            {
                { nameof(ProfileInfoDto.ProfileId), nameof(ProfileData.Created) },
                { nameof(ProfileInfoDto.FullName), nameof(ProfileData.InsuredName) },
            };

        public ProfileDataRepository(IApplicationDbContext context) : base(context)
        {
        }

        public override Task<ProfileData> GetByIdAsync(Guid id)
        {
            return DbSet.Include(x => x.StateInfo).Include(x => x.IntakeForm).ThenInclude(x => x.IntakeFormTypesOfLossInfo).ThenInclude(x => x.TypeOfLossInfo).FirstOrDefaultAsync(x => x.Id == id);
        }

        private IQueryable<ProfileData> GetFilteredQuery(IQueryable<ProfileData> dataSource,
         PaginationInfoProfileDto pagination)
        {
            return dataSource.Where(x => pagination.Roles == null ||
                           pagination.Roles.Length == 0 ||
                           x.Roles.Any((y =>
                               pagination.Roles.Contains(y))));
        }

        public override async Task<PaginatedDataDto<ProfileData>> GetPaginatedDataAsync(PaginationInfoProfileDto pagination)
        {
            var baseQuery = DbSet
                .Include(x => x.IntakeForm)
                .ThenInclude(x => x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x => x.TypeOfLossInfo)
                .AsQueryable();

           baseQuery = GetFilteredQuery(baseQuery, pagination);

            var count = await baseQuery.CountAsync();
            var result = await GetPaginatedQuery(baseQuery, pagination).ToListAsync();

            return new PaginatedDataDto<ProfileData>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                TotalCount = count
            };
        }
    }
}