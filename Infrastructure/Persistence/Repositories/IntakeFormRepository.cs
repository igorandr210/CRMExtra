using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Quartz.Util;

namespace Infrastructure.Persistence.Repositories
{

    public class IntakeFormRepository : IIntakeFormRepository
    {
        private readonly IApplicationDbContext _context;

        public IntakeFormRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IntakeForm> UpdateFormAsync(IntakeForm updateData, CancellationToken cancellationToken)
        {
            var existedIntakeForm = await _context.IntakeForms
                .AsNoTracking()
                .Include(x => x.Claim)
                .Include(x => x.Mortgage)
                .Include(x => x.InsuranceAgency)
                .Include(x => x.Spouse)
                .Include(x => x.User)
                .Include(x => x.ClaimChecks)
                .ThenInclude(x => x.Document)
                .Include(x => x.IntakeFormTypesOfLossInfo)
                .ThenInclude(x => x.TypeOfLossInfo)
                .Include(x => x.RoofDamageInfo)
                .Select(x => new
                {
                    x.UserId,
                    IntakeFormId = x != null ? x.Id : (Guid?)null,
                    ClaimId = x.Claim != null ? x.Claim.Id : (Guid?)null,
                    MortgageId = x.Mortgage != null ? x.Mortgage.Id : (Guid?)null,
                    InsuranceAgencyId = x.InsuranceAgency != null ? x.InsuranceAgency.Id : (Guid?)null,
                    SpouseId = x.Spouse != null ? x.Spouse.Id : (Guid?)null,
                    ClaimChecks = x.ClaimChecks,
                    IntakeFormTypesOfLoss = x.IntakeFormTypesOfLossInfo,
                    RoofDamageId = x.RoofDamageInfo != null ? x.Spouse.Id : (Guid?)null,
                })
                .FirstOrDefaultAsync(x => x.UserId == updateData.UserId, cancellationToken);

            foreach (var updateClaimCheck in updateData.ClaimChecks)
            {
                var existedClaimCheck = existedIntakeForm?.ClaimChecks.FirstOrDefault(x => x.Id == updateClaimCheck.Id);
                var claimCheckDocument =
                    await _context.Documents.FirstOrDefaultAsync(x => x.Id == updateClaimCheck.DocumentId);

                updateClaimCheck.Document = claimCheckDocument;

                AttachToContext(updateClaimCheck, existedClaimCheck?.Id);
            }

            var claimChecksToDelete = existedIntakeForm?.ClaimChecks.Except(updateData.ClaimChecks);
            if (claimChecksToDelete is not null)
            {
                _context.ClaimChecks.RemoveRange(claimChecksToDelete);
            }

            AttachToContext(updateData.Spouse, existedIntakeForm?.SpouseId);
            AttachToContext(updateData.Claim, existedIntakeForm?.ClaimId);
            AttachToContext(updateData.InsuranceAgency, existedIntakeForm?.InsuranceAgencyId);
            AttachToContext(updateData.Mortgage, existedIntakeForm?.MortgageId);
            AttachToContext(updateData.RoofDamageInfo, existedIntakeForm?.RoofDamageId);
            _context.Entry(updateData).State = EntityState.Modified;
            if (!updateData.User.Email.IsNullOrWhiteSpace())
            {
                AttachToContext(updateData.User, updateData.UserId);
                _context.Entry(updateData.User).Property(x => x.Email).IsModified = false;
                _context.Entry(updateData.User).Property(x => x.IdForYear).IsModified = false;
                _context.Entry(updateData.User).Property(x => x.Id).IsModified = false;
                _context.Entry(updateData.User).Property(x => x.Roles).IsModified = false;
                _context.Entry(updateData.User).Property(x => x.Created).IsModified = false;
                _context.Entry(updateData.User).Property(x => x.CreatedBy).IsModified = false;
            }

            var entity = AttachToContext(updateData, existedIntakeForm?.IntakeFormId, true);
            
            foreach (var updateIntakeFormTypeOfLoss in updateData.IntakeFormTypesOfLossInfo)
            {
                updateIntakeFormTypeOfLoss.IntakeFormId = entity.Id;

                var typeOfLoss =
                    _context.TypesOfLoss.FirstOrDefault(x => x.Id == updateIntakeFormTypeOfLoss.TypeOfLossId);
                updateIntakeFormTypeOfLoss.TypeOfLossInfo = typeOfLoss;
            }

            if (existedIntakeForm is not null)
            {
                var intakeFormTypesOfLossToAdd =
                    updateData.IntakeFormTypesOfLossInfo.Except(existedIntakeForm.IntakeFormTypesOfLoss);
                _context.IntakeFormTypeOfLosses.AddRange(intakeFormTypesOfLossToAdd);

                var intakeFormTypesOfLossToDelete =
                    existedIntakeForm.IntakeFormTypesOfLoss.Except(updateData.IntakeFormTypesOfLossInfo);
                _context.IntakeFormTypeOfLosses.RemoveRange(intakeFormTypesOfLossToDelete);
            }
            else
            {
                await _context.IntakeFormTypeOfLosses.AddRangeAsync(updateData.IntakeFormTypesOfLossInfo);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IntakeForm> SubmitReviewFormAsync(Guid userId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var existedIntakeForm = await _context.IntakeForms
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            existedIntakeForm.IsConfirmed = true;

            await _context.SaveChangesAsync(cancellationToken);

            return existedIntakeForm;
        }

        private T AttachToContext<T>(T data, Guid? id, bool updateKey = true)
            where T : BaseEntity
        {
            EntityEntry<T> entityEntry = null;
            var set = _context.Set<T>();

            if (data is not null)
            {
                if (id is not null && updateKey)
                {
                    data.Id = id.Value;
                }

                entityEntry = set.Attach(data);
            }

            switch (data is not null, id is not null)
            {
                case (true, true):
                    entityEntry!.CurrentValues.SetValues(data!);
                    entityEntry!.State = EntityState.Modified;
                    return entityEntry.Entity;
                case (false, true):
                    return set.Remove(set.Find(id)).Entity;
                case (true, false):
                    entityEntry!.State = EntityState.Added;
                    return entityEntry.Entity;
                case (_, _):
                    return data;
            }
        }
    }
}