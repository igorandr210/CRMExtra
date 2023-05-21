using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Guid UserId => GetUserId(_httpContext?.HttpContext?.User.Claims);
        public Role[] Roles => GetUserRoles(_httpContext?.HttpContext?.User.Claims);
        public bool HasRole(Role role) => Roles.Contains(role);
        
        private Guid GetUserId(IEnumerable<Claim> claims)
        {
            var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToString() ?? string.Empty;
            return Guid.Parse(userId);
        }
        
        private Role[] GetUserRoles(IEnumerable<Claim> claims)
        {
            var userRoles = claims.Where(x => x.Type == "cognito:groups")
                .SelectMany(x=>x.Value.Split(","))
                .Select(x=> Enum.Parse<Role>(x.Trim()))
                .ToArray();
            
            return userRoles;
        }
    }
}
