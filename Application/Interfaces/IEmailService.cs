using Application.Common.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(SendEmailDto emailInfo);
        Task<bool> IsVerified(string email);
        Task<bool> Verify(string email);
    }
}
