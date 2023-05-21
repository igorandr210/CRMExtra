using System;
using System.Threading.Tasks;
using Application.Authorization.DTOs;
using Application.Common.DTOs;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<CreateUserResultDto> CreateUser(SignUpDto signupDto);
        Task ForgotPassword(string email);
        Task<LoginResponseDto> LoginUser(LoginDto loginDto);
        Task DeleteUser(string email);
    }
}