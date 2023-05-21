using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<InsuranceAgencyInfo> InsuranceAgencies { get; set; }
        public virtual DbSet<IntakeForm> IntakeForms { get; set; }
        public virtual DbSet<MortgageInfo> Mortgages { get; set; }
        public virtual DbSet<ProfileData> ProfileData { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<JobCronSetting> JobCronSettings { get; set; }
        public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public virtual DbSet<TypeOfLoss> TypesOfLoss { get; set; }
        public virtual DbSet<TypeOfOccupation> TypesOfOccupation { get; set; }
        public virtual DbSet<Storm> Storms { get; set; }
        public virtual DbSet<Spouse> Spouses { get; set; }
        public virtual DbSet<Envelope> Envelopes { get; set; }
        public virtual DbSet<SelfOccupation> SelfOccupations { get; set; }
        public virtual DbSet<AssignmentTask> AssignmentTasks { get; set; }
        public virtual DbSet<ClaimCheck> ClaimChecks { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<RoofDamage> RoofDamages { get; set; }

        public virtual DbSet<IntakeFormTypeOfLoss> IntakeFormTypeOfLosses { get; set; }
        public virtual DbSet<AssingmentTaskAttachments> AssingmentTasksAttachments { get; set; }

        private void SoftDelete()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                if (entity.Entity is ProfileData)
                {
                    var userData = entity.Entity as ProfileData;

                    entity.State = EntityState.Modified;
                    userData.IsDeleted = true;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            await _mediator.DispatchDomainEvents(this);

            SoftDelete();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");
            modelBuilder.Entity<ProfileData>().HasQueryFilter(b => !b.IsDeleted);

            modelBuilder.Entity<JobCronSetting>(entity =>
            {
                entity.ToTable("jobcronsettings");
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                
                entity.Property(e => e.JobType).HasColumnName("jobtype");
                
                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.CronSettingString).HasColumnName("cronsettingstring");
                
                entity.Property(e => e.LastRunned).HasColumnName("lastrunned");
                
                entity.Property(e => e.ErrorNote).HasColumnName("errornote");
            });
            
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("claims");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ClaimChecks).HasColumnName("claimchecks");

                entity.Property(e => e.ClaimFilled).HasColumnName("claimfilled");

                entity.Property(e => e.ClaimFilledDate)
                    .HasColumnType("date")
                    .HasColumnName("claimfilleddate");

                entity.Property(e => e.ClaimInfo)
                    .HasColumnType("character varying")
                    .HasColumnName("claiminfo");

                entity.Property(e => e.ClaimNumber)
                    .HasColumnType("character varying")
                    .HasColumnName("claimnumber");

                entity.Property(e => e.IntakeFormId).HasColumnName("intakeformid");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");


                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.IntakeForm)
                    .WithOne(p => p.Claim)
                    .HasForeignKey<Claim>(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("claims_intakeform_fkey");
            });
            modelBuilder.Entity<ClaimCheck>(entity =>
            {
                entity.ToTable("claimcheck");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.DateOfPostmark)
                    .HasColumnType("timestamp")
                    .HasColumnName("dateofpostmark");

                entity.Property(e => e.ClaimAmount).HasColumnName("claimamount");

                entity.Property(e => e.ClaimDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("claimdate");

                entity.Property(e => e.IntakeFormId)
                   .HasColumnName("intakeformid");

                entity.Property(e => e.DocumentId)
                   .HasColumnName("documentid");

                entity.Property(e => e.Created)
                   .HasColumnType("timestamp")
                   .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.Document)
                       .WithOne(x=>x.ClaimCheck)
                       .HasForeignKey<ClaimCheck>(d => d.DocumentId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("claimcheck_documents_fkey");

                entity.HasOne(d => d.IntakeForm)
                      .WithMany(x => x.ClaimChecks)
                      .HasForeignKey(d => d.IntakeFormId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("claimcheck_intakeform_fkey");

            });
            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("documents");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Key).HasColumnName("key");

                entity.Property(e => e.FileName).HasColumnName("filename");

                entity.Property(e => e.DocumentType)
                    .HasColumnType("int")
                    .HasColumnName("documenttype");

                entity.Property(e => e.IntakeFormId)
                    .HasColumnName("intakeformid");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.IntakeForm)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("documents_intakeform_fkey");
            });

            modelBuilder.Entity<InsuranceAgencyInfo>(entity =>
            {
                entity.ToTable("insuranceagencies");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.AgentEmail)
                    .HasColumnType("character varying")
                    .HasColumnName("agentemail");

                entity.Property(e => e.AgentPhone)
                    .HasColumnType("character varying")
                    .HasColumnName("agentphone");

                entity.Property(e => e.InsuranceAgency)
                    .HasColumnType("character varying")
                    .HasColumnName("insuranceagency");

                entity.Property(e => e.IntakeFormId).HasColumnName("intakeformid");

                entity.Property(e => e.NameOfAgent)
                    .HasColumnType("character varying")
                    .HasColumnName("nameofagent");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.IntakeForm)
                    .WithOne(p => p.InsuranceAgency)
                    .HasForeignKey<InsuranceAgencyInfo>(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("insuranceagencies_intakeform_fkey");
            });

            modelBuilder.Entity<Envelope>(entity =>
            {
                entity.ToTable("envelopes");

                entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

                entity.Property(e => e.IsAdminSign)
                .HasColumnName("isadminsign");

                entity.Property(e => e.IsCustomerSign)
                .HasColumnName("iscustomersign");

                entity.Property(e => e.IntakeFormId)
               .HasColumnName("intakeformid");

                entity.Property(e => e.CustomersEnvelopeLink)
               .HasColumnName("customersenvelopelink");

                entity.HasOne(d => d.IntakeForm)
                    .WithOne(p => p.Envelope)
                    .HasForeignKey<Envelope>(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("intakeforms_envelope_fkey");
               
            });

            modelBuilder.Entity<IntakeForm>(entity =>
            {
                entity.ToTable("intakeforms");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ContentsDamage).HasColumnName("contentsdamage");

                entity.Property(e => e.DateOfLoss)
                    .HasColumnType("date[]")
                    .HasColumnName("dateofloss");

                entity.Property(e => e.DateOfLossWithin).HasColumnName("dateoflosswithin");

                entity.Property(e => e.EffectivePolicyEndDate)
                    .HasColumnType("date")
                    .HasColumnName("effactivepolicyenddate");

                entity.Property(e => e.RoofReplaceDate)
                    .HasColumnType("date")
                    .HasColumnName("roofreplacedate");

                entity.Property(e => e.EffectivePolicyStartDate)
                    .HasColumnType("date")
                    .HasColumnName("effactivepolicystartdate");

                entity.Property(e => e.EmergencyService).HasColumnName("emergencyservice");

                entity.Property(e => e.ExteriorDamage).HasColumnName("exteriordamage");

                entity.Property(e => e.InsuranceCompany)
                    .HasColumnName("insurancecompany");

                entity.Property(e => e.InteriorDamage).HasColumnName("interiordamage");

                entity.Property(e => e.IsConfirmed).HasColumnName("isconfirmed");

                entity.Property(e => e.IsDraft).HasColumnName("isdraft");

                entity.Property(e => e.IsFilled).HasColumnName("isfilled");

                entity.Property(e => e.MailAddress)
                    .HasColumnType("character varying")
                    .HasColumnName("mailaddress");

                entity.Property(e => e.PolicyNumber)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("policynumber");

                entity.Property(e => e.PrimaryResidence).HasColumnName("primaryresidence");

                entity.Property(e => e.PropertyAccess)
                    .HasColumnType("character varying")
                    .HasColumnName("propertyaccess");

                entity.Property(e => e.IsPropertyOccupied).HasColumnName("ispropertyoccupied");

                entity.Property(e => e.PropertyOwner).HasColumnName("propertyowner");

                entity.Property(e => e.RoofDamage).HasColumnName("roofdamage");

                entity.Property(e => e.SelfOccupation).HasColumnName("selfoccupation");

                entity.Property(e => e.StormName).HasColumnName("stormname");

                entity.Property(e => e.TypeOfOccupation).HasColumnName("typeofoccupation");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.IntakeForm)
                    .HasForeignKey<IntakeForm>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("oprofiledata_intakeforms_fkey");

                entity.HasOne(d => d.SelfOccupationInfo)
                    .WithMany()
                    .HasForeignKey(d => d.SelfOccupation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selfoccupation_intakeforms_fk");

                entity.HasOne(d => d.RoofDamageInfo)
                   .WithOne(p => p.IntakeForm)
                   .HasForeignKey<IntakeForm>(d => d.RoofDamage)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("roofdamage_intakeforms_fk");

                entity.HasOne(d => d.StormInfo)
                    .WithMany()
                    .HasForeignKey(d => d.StormName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("storms_intakeforms_fk");

                entity.HasOne(d => d.TypeOfOccupationInfo)
                    .WithMany()
                    .HasForeignKey(d => d.TypeOfOccupation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("occupation_intakeforms_fk");

                entity.HasOne(d => d.InsuranceCompanyInfo)
                    .WithMany()
                    .HasForeignKey(d => d.InsuranceCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("company_intakeforms_fk");
            });

            modelBuilder.Entity<IntakeFormTypeOfLoss>(entity =>
            {
                entity.ToTable("intakeformstypeofloss");

                entity.Ignore(e => e.Id);
                
                entity.Property(e => e.IntakeFormId)
                    .HasColumnName("intakeformid");
                
                entity.Property(e => e.TypeOfLossId)
                    .HasColumnName("typeoflossid");
                
                modelBuilder.Entity<IntakeFormTypeOfLoss>()
                    .HasKey(bc => new { bc.IntakeFormId, bc.TypeOfLossId }); 
                
                modelBuilder.Entity<IntakeFormTypeOfLoss>()
                    .HasOne(sc => sc.TypeOfLossInfo)
                    .WithMany(s => s.IntakeFormTypeofLossInfo)
                    .HasForeignKey(sc => sc.TypeOfLossId);
                
                modelBuilder.Entity<IntakeFormTypeOfLoss>()
                    .HasOne(sc => sc.IntakeFormInfo)
                    .WithMany(s => s.IntakeFormTypesOfLossInfo)
                    .HasForeignKey(sc => sc.IntakeFormId);
                
                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");
            });
            modelBuilder.Entity<AssingmentTaskAttachments>(entity =>
            {
                entity.ToTable("assingmenttaskattachments");

                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("id");

                entity.Property(e => e.AssingmentTaskId)
                    .HasColumnName("assingmenttaskid");

                entity.Property(e => e.FileName)
                    .HasColumnName("filename");

                entity.Property(e => e.Key)
                    .HasColumnName("key");

                modelBuilder.Entity<AssingmentTaskAttachments>()
                    .HasOne(sc => sc.AssignmentTaskInfo)
                    .WithMany(s => s.AssingmentTaskAttachmentsInfo)
                    .HasForeignKey(sc => sc.AssingmentTaskId);

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");
            });
            modelBuilder.Entity<SelfOccupation>(entity =>
            {
                entity.ToTable("selfoccupation");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });
            modelBuilder.Entity<RoofDamage>(entity =>
            {
                entity.ToTable("roofdamage");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });
            modelBuilder.Entity<MortgageInfo>(entity =>
            {
                entity.ToTable("mortgages");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Companyname)
                    .HasColumnType("character varying")
                    .HasColumnName("companyname");

                entity.Property(e => e.ContractorEmail)
                    .HasColumnType("character varying")
                    .HasColumnName("contractoremail");

                entity.Property(e => e.ContractorName)
                    .HasColumnType("character varying")
                    .HasColumnName("contractorname");

                entity.Property(e => e.IntakeFormId).HasColumnName("intakeformid");

                entity.Property(e => e.LoanNumber)
                    .HasColumnType("character varying")
                    .HasColumnName("loannumber");

                entity.Property(e => e.Mortgage).HasColumnName("mortgage");

                entity.Property(e => e.ReferredBy)
                    .HasColumnType("character varying")
                    .HasColumnName("refferedby");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.IntakeForm)
                    .WithOne(p => p.Mortgage)
                    .HasForeignKey<MortgageInfo>(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mortgages_intakeform_fkey");
            });

            modelBuilder.Entity<Spouse>(entity =>
            {
                entity.ToTable("spouses");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IsSpouse)
                    .HasColumnName("isspouse");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasColumnType("character varying")
                    .HasColumnName("phone");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.IntakeFormId).HasColumnName("intakeformid");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.IntakeForm)
                    .WithOne(p => p.Spouse)
                    .HasForeignKey<Spouse>(d => d.IntakeFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("spouses_intakeform_fkey");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("states");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");
            });

            modelBuilder.Entity<InsuranceCompany>(entity =>
            {
                entity.ToTable("insurance_companies");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });

            modelBuilder.Entity<TypeOfLoss>(entity =>
            {
                entity.ToTable("type_of_loss");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });

            modelBuilder.Entity<TypeOfOccupation>(entity =>
            {
                entity.ToTable("type_of_occupation");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });

            modelBuilder.Entity<Storm>(entity =>
            {
                entity.ToTable("storms");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdby");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");
            });

            modelBuilder.Entity<ProfileData>(entity =>
            {
                entity.ToTable("profiledata");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.City)
                    .HasColumnName("city");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone");

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("addressline1");

                entity.Property(e => e.State)
                    .HasColumnType("uuid")
                    .HasColumnName("state");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip");

                entity.Property(e => e.InsuredName)
                    .HasColumnName("insuredname");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.IsDeleted)
                .HasColumnName("isdeleted");

                entity.Property(e => e.Roles)
                .HasColumnName("roles");

                entity.Property(e => e.IdForYear)
               .HasColumnName("idforyear");

                entity.HasOne(d => d.StateInfo)
                    .WithMany()
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profiledata_fk");

                entity.Property(e => e.LastModifiedBy).HasColumnName("lastmodifiedby");
            });
            modelBuilder.Entity<AssignmentTask>(entity =>
            {
                entity.ToTable("assignmenttasks");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy).HasColumnName("createdby");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status");

                entity.Property(e => e.AssignedToId)
                    .HasColumnName("assignedtoid");

                entity.Property(e => e.TaskTypeId)
                   .HasColumnName("tasktypeid");

                entity.Property(e => e.AttachedProfileId)
                  .HasColumnName("attachedprofileid");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastmodifiedby");

                entity.HasOne(d => d.Creator)
                    .WithMany(d => d.CreatedTasks)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignmenttask_creatorprofiledata_fk");

                entity.HasOne(d => d.AssignedUser)
                    .WithMany(d => d.AssignedTasks)
                    .HasForeignKey(d => d.AssignedToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignmenttask_assigneeprofiledata_fk");

                entity.HasOne(d => d.AttachedProfile)
                    .WithMany(d => d.AttachedToTasks)
                    .HasForeignKey(d => d.AttachedProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignmenttask_attachedprofiledata_fk");

                entity.HasOne(d => d.TaskType)
                    .WithMany(d => d.AssignmentTasks)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignmenttask_tasktype_fk");
            });
            modelBuilder.Entity<TaskType>(entity =>
                {
                    entity.ToTable("tasktype");

                    entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    entity.Property(e => e.Value)
                        .HasColumnName("value");

                    entity.Property(e => e.LastModified)
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");
                
                    entity.Property(e => e.LastModifiedBy)
                        .HasColumnName("lastmodifiedby");
                });
        }
    }
}
