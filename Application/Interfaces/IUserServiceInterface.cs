using System;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Guid UserId { get; }
        public Role[] Roles { get; }
        public bool HasRole(Role role);
    }
}
