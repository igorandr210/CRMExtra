using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ProfileData> ProfileData { get; set; }
        DbSet<Claim> Claims { get; set; }
        DbSet<Spouse> Spouses { get; set; }
        DbSet<InsuranceAgencyInfo> InsuranceAgencies { get; set; }
        DbSet<IntakeForm> IntakeForms { get; set; }
        DbSet<JobCronSetting> JobCronSettings { get; set; }
        DbSet<MortgageInfo> Mortgages { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<State> States { get; set; }
        DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        DbSet<TypeOfLoss> TypesOfLoss { get; set; }
        DbSet<TypeOfOccupation> TypesOfOccupation { get; set; }
        DbSet<Storm> Storms { get; set; }
        DbSet<Envelope> Envelopes { get; set; }
        DbSet<SelfOccupation> SelfOccupations { get; set; }
        DbSet<AssignmentTask> AssignmentTasks { get; set; }
        DbSet<ClaimCheck> ClaimChecks { get; set; }
        DbSet<TaskType> TaskTypes { get; set; }
        DbSet<IntakeFormTypeOfLoss> IntakeFormTypeOfLosses { get; set; }
        DbSet<AssingmentTaskAttachments> AssingmentTasksAttachments { get; set; }
        DbSet<RoofDamage> RoofDamages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ());
        DbSet<T> Set<T>() where T:class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
