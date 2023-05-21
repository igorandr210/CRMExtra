using Application.Common.DTOs;
using Domain.Enum;
using System.Collections.Generic;

namespace Application.ProfilesData.Dto
{
    public class PaginationInfoProfileDto : PaginationInfoDto
    {
        public Role[] Roles { get; set; } = new Role[] {Role.Admin, Role.Employee, Role.Customer };
    }
}
