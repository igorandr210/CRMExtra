using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.AssignmentTasks.DTOs;
using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AssignmentTaskRepository : PaginatedRepository<AssignmentTask, PaginationInfoTaskDto>
    {
        private readonly IUserService _userService;

        protected override Dictionary<string, string> SortColumnMapping => new(StringComparer.OrdinalIgnoreCase)
        {
            {
                nameof(GetTasksResponseDto.TaskType),
                $"{nameof(AssignmentTask.TaskType)}.{nameof(AssignmentTask.TaskType.Value)}"
            },
            { nameof(GetTasksResponseDto.Label), nameof(AssignmentTask.Name) },
            {
                nameof(GetTasksResponseDto.CreatedBy),
                $"{nameof(AssignmentTask.Creator)}.{nameof(AssignmentTask.Creator.InsuredName)}"
            },
            {
                nameof(GetTasksResponseDto.AssignedTo),
                $"{nameof(AssignmentTask.AssignedUser)}.{nameof(AssignmentTask.AssignedUser.InsuredName)}"
            },
        };

        public AssignmentTaskRepository(IApplicationDbContext context, IUserService userService) : base(context)
        {
            _userService = userService;
        }

        public override async Task<PaginatedDataDto<AssignmentTask>> GetPaginatedDataAsync(
            PaginationInfoTaskDto pagination)
        {
            var baseQuery = DbSet
                .Include(x => x.AssignedUser)
                .ThenInclude(x => x.IntakeForm)
                .Include(x => x.Creator)
                .Include(x => x.AttachedProfile)
                .ThenInclude(x => x.IntakeForm)
                .ThenInclude(x=>x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x=>x.TypeOfLossInfo)
                .Include(x => x.TaskType)
                .AsQueryable();

            baseQuery = GetFilteredQuery(baseQuery, pagination);

            var count = await baseQuery.CountAsync();

            var result = await GetPaginatedQuery(baseQuery, pagination).ToListAsync();

            return new PaginatedDataDto<AssignmentTask>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                TotalCount = count
            };
        }

        private IQueryable<AssignmentTask> GetFilteredQuery(IQueryable<AssignmentTask> dataSource,
            PaginationInfoTaskDto pagination)
        {
            var currentUserId = _userService.UserId;
            return dataSource
                .Where(x => string.IsNullOrWhiteSpace(pagination.AttachedProfileSearchString) ||
                            x.AttachedProfile.InsuredName.ToLower()
                                .Contains(pagination.AttachedProfileSearchString.ToLower()) ||
                            x.AttachedProfile.IdForYear.ToLower()
                                .Contains(pagination.AttachedProfileSearchString.ToLower()))
                .Where(x => string.IsNullOrWhiteSpace(pagination.AssignedProfileSearchString) ||
                            x.AssignedUser.InsuredName.ToLower()
                                .Contains(pagination.AssignedProfileSearchString.ToLower()) ||
                            x.AssignedUser.IdForYear.ToLower()
                                .Contains(pagination.AssignedProfileSearchString.ToLower()))
                .Where(x => string.IsNullOrWhiteSpace(pagination.CreatorProfileSearchString) ||
                            x.Creator.InsuredName.ToLower().Contains(pagination.CreatorProfileSearchString.ToLower()) ||
                            x.Creator.IdForYear.ToLower().Contains(pagination.CreatorProfileSearchString.ToLower()))
                .Where(x => pagination.Statuses.Length == 0 ||
                            pagination.Statuses.Contains(x.Status))
                .Where(x => pagination.DateOfLoss == null ||
                            x.AttachedProfile.IntakeForm.DateOfLoss.Any(d => d.Date == pagination.DateOfLoss))
                .Where(x => pagination.TasksOwnership == null ||
                            pagination.TasksOwnership == TasksOwnership.AssignedToUser &&
                            x.AssignedUser.Id == currentUserId ||
                            pagination.TasksOwnership == TasksOwnership.CreatedByUser && x.Creator.Id == currentUserId)
                .Where(x => pagination.TypesOfLoss == null ||
                            pagination.TypesOfLoss.Length == 0 ||
                            x.AttachedProfile.IntakeForm.IntakeFormTypesOfLossInfo.Any(y =>
                                pagination.TypesOfLoss.Contains(y.TypeOfLossId)))
                .Where(x => !x.AttachedProfile.IsDeleted);
        }

        public override Task<AssignmentTask> GetByIdAsync(Guid id)
        {
            return DbSet.Include(x => x.TaskType)
                .Include(x => x.AssignedUser)
                .Include(x => x.AttachedProfile)
                .ThenInclude(x => x.IntakeForm)
                .ThenInclude(x => x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x => x.TypeOfLossInfo)
                .Include(x => x.AssingmentTaskAttachmentsInfo)
                .Include(x => x.Creator).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}