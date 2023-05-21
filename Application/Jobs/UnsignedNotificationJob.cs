using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Application.Jobs
{
    public class UnsignedNotificationJob : IJob
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IEmailService _emailService;
        
        private const string CrmEmail = "crmdevelopment2022@gmail.com";

        public UnsignedNotificationJob(IApplicationDbContext applicationDbContext, IEmailService emailService)
        {
            _applicationDbContext = applicationDbContext;
            _emailService = emailService;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            var jobModel =
                await _applicationDbContext.JobCronSettings.FirstOrDefaultAsync(j =>
                    j.JobType == JobType.ReminderJob);
            try
            {
                var unsignedAdminDocs = await _applicationDbContext.Envelopes
                    .Where(x => !x.IsAdminSign)
                    .ToListAsync();
                var unsignedCustomerDocs = await _applicationDbContext.Envelopes
                    .Where(x => !x.IsCustomerSign)
                    .ToListAsync();

                if (unsignedAdminDocs != null && unsignedAdminDocs.Any())
                {
                    var adminsEmails = await _applicationDbContext.ProfileData
                        .Where(x => x.Roles.Contains(Role.Admin))
                        .Select(x => x.Email)
                        .ToListAsync();

                    foreach (var email in adminsEmails)
                    {
                        var envelope = await _applicationDbContext.Envelopes
                            .Include(x => x.IntakeForm).ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(x => x.IntakeForm.User.Email == email);
                        await _emailService.SendEmail(new SendEmailDto
                        {
                            From = CrmEmail,
                            Destination = email,
                            Message = envelope.CustomersEnvelopeLink,
                            Subject = $"You have unsigned {unsignedAdminDocs.Count} documents"
                        });
                    }
                }

                if (unsignedCustomerDocs != null && unsignedCustomerDocs.Any())
                {
                    var customersDocs = _applicationDbContext.IntakeForms
                        .Where(x => unsignedCustomerDocs.Select(envelope => envelope.IntakeFormId).Contains(x.Id));

                    var customerEmail = customersDocs.Select(x => x.User.Email);

                    foreach (var email in customerEmail)
                    {
                        var formId = (await customersDocs.FirstOrDefaultAsync(x => x.User.Email == email)).Id;
                        await _emailService.SendEmail(new SendEmailDto
                        {
                            From = CrmEmail, Destination = email,
                            Message = $"Here must be links for this form id {formId}!",
                            Subject = "You have unsigned form"
                        });
                    }
                }
                
                jobModel.ErrorNote = null;
            }
            catch (Exception e)
            {
                jobModel.ErrorNote = e.Message;
            }
            finally
            {
                jobModel.LastRunned = DateTime.Now;

                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}