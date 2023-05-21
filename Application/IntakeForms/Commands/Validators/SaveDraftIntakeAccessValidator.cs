using System.Threading.Tasks;
using Application.Common.Behaviours;
using Application.IntakeForms.DTOs;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.IntakeForms.Commands
{
    public class SaveDraftIntakeAccessValidator : IAccessValidator<SaveDraftIntakeFormCommand, IntakeFormResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public SaveDraftIntakeAccessValidator(IApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<bool> ValidateAccess(SaveDraftIntakeFormCommand request)
        {
            var userId = request.UserId;
            var sameUser = _userService.UserId == request.UserId;
            var isAdmin = _userService.HasRole(Role.Employee) || _userService.HasRole(Role.Admin);
            var existedIntakeForm = await _context.IntakeForms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (existedIntakeForm is null)
                return sameUser || isAdmin;

            if (existedIntakeForm.IsFilled && existedIntakeForm.IsConfirmed)
                return false;

            if (existedIntakeForm.IsFilled)
                return isAdmin;
             
            return true;
        }
    }
}