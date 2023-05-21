using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IIntakeFormRepository
    {
        Task<IntakeForm> UpdateFormAsync(IntakeForm updateData, CancellationToken cancellationToken = new());
        Task<IntakeForm> SubmitReviewFormAsync(Guid userId, CancellationToken cancellationToken = new());
    }
}