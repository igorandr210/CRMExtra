using Domain.Enum;
using System;

namespace Application.Common.DTOs
{
    public class CreateUserResultDto
    {
        public Guid UserId { get; init; }
        public bool IsSuccess { get; init; }
        public Role Role { get; set; }
    }
}