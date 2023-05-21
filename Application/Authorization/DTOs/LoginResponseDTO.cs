using System;

namespace Application.Authorization.DTOs
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string Email { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
