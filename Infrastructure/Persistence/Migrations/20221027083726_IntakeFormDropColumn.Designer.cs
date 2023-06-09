﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221027083726_IntakeFormDropColumn")]
    partial class IntakeFormDropColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "English_United States.1252")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Entities.Claim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("ClaimChecks")
                        .HasColumnType("boolean")
                        .HasColumnName("claimchecks");

                    b.Property<bool>("ClaimFilled")
                        .HasColumnType("boolean")
                        .HasColumnName("claimfilled");

                    b.Property<DateTime?>("ClaimFilledDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("claimfilleddate");

                    b.Property<string>("ClaimInfo")
                        .HasColumnType("character varying")
                        .HasColumnName("claiminfo");

                    b.Property<string>("ClaimNumber")
                        .HasColumnType("character varying")
                        .HasColumnName("claimnumber");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId")
                        .IsUnique();

                    b.ToTable("claims");
                });

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long?>("ClaimAmount")
                        .HasColumnType("bigint")
                        .HasColumnName("claimamount");

                    b.Property<DateTime?>("ClaimDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("claimdate");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int")
                        .HasColumnName("documenttype");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("Domain.Entities.Envelope", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<bool>("IsAdminSign")
                        .HasColumnType("boolean")
                        .HasColumnName("isadminsign");

                    b.Property<bool>("IsCustomerSign")
                        .HasColumnType("boolean")
                        .HasColumnName("iscustomersign");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId")
                        .IsUnique();

                    b.ToTable("envelopes");
                });

            modelBuilder.Entity("Domain.Entities.InsuranceAgencyInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AgentEmail")
                        .HasColumnType("character varying")
                        .HasColumnName("agentemail");

                    b.Property<string>("AgentPhone")
                        .HasColumnType("character varying")
                        .HasColumnName("agentphone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<string>("InsuranceAgency")
                        .HasColumnType("character varying")
                        .HasColumnName("insuranceagency");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("NameOfAgent")
                        .HasColumnType("character varying")
                        .HasColumnName("nameofagent");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId")
                        .IsUnique();

                    b.ToTable("insuranceagencies");
                });

            modelBuilder.Entity("Domain.Entities.InsuranceCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("insurance_companies");
                });

            modelBuilder.Entity("Domain.Entities.IntakeForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("ContentsDamage")
                        .HasColumnType("boolean")
                        .HasColumnName("contentsdamage");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime[]>("DateOfLoss")
                        .HasColumnType("timestamp[]")
                        .HasColumnName("dateofloss");

                    b.Property<bool?>("DateOfLossWithin")
                        .HasColumnType("boolean")
                        .HasColumnName("dateoflosswithin");

                    b.Property<DateTime?>("EffectivePolicyEndDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("effactivepolicyenddate");

                    b.Property<DateTime?>("EffectivePolicyStartDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("effactivepolicystartdate");

                    b.Property<bool>("EmergencyService")
                        .HasColumnType("boolean")
                        .HasColumnName("emergencyservice");

                    b.Property<bool>("ExteriorDamage")
                        .HasColumnType("boolean")
                        .HasColumnName("exteriordamage");

                    b.Property<Guid?>("InsuranceCompany")
                        .HasColumnType("uuid")
                        .HasColumnName("insurancecompany");

                    b.Property<bool>("InteriorDamage")
                        .HasColumnType("boolean")
                        .HasColumnName("interiordamage");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("isconfirmed");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("boolean")
                        .HasColumnName("isdraft");

                    b.Property<bool>("IsFilled")
                        .HasColumnType("boolean")
                        .HasColumnName("isfilled");

                    b.Property<bool?>("IsPropertyOccupied")
                        .HasColumnType("boolean")
                        .HasColumnName("ispropertyoccupied");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("MailAddress")
                        .HasColumnType("character varying")
                        .HasColumnName("mailaddress");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("policynumber");

                    b.Property<bool?>("PrimaryResidence")
                        .HasColumnType("boolean")
                        .HasColumnName("primaryresidence");

                    b.Property<string>("PropertyAccess")
                        .HasColumnType("character varying")
                        .HasColumnName("propertyaccess");

                    b.Property<bool?>("PropertyOwner")
                        .HasColumnType("boolean")
                        .HasColumnName("propertyowner");

                    b.Property<bool>("RoofDamage")
                        .HasColumnType("boolean")
                        .HasColumnName("roofdamage");

                    b.Property<DateTime?>("RoofReplaceDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("roofreplacedate");

                    b.Property<Guid?>("SelfOccupation")
                        .HasColumnType("uuid")
                        .HasColumnName("selfoccupation");

                    b.Property<Guid?>("StormName")
                        .HasColumnType("uuid")
                        .HasColumnName("stormname");

                    b.Property<Guid?>("TypeOfLoss")
                        .HasColumnType("uuid")
                        .HasColumnName("typeofloss");

                    b.Property<Guid?>("TypeOfOccupation")
                        .HasColumnType("uuid")
                        .HasColumnName("typeofoccupation");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceCompany");

                    b.HasIndex("StormName");

                    b.HasIndex("TypeOfLoss");

                    b.HasIndex("TypeOfOccupation");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("intakeforms");
                });

            modelBuilder.Entity("Domain.Entities.MortgageInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Companyname")
                        .HasColumnType("character varying")
                        .HasColumnName("companyname");

                    b.Property<string>("ContractorEmail")
                        .HasColumnType("character varying")
                        .HasColumnName("contractoremail");

                    b.Property<string>("ContractorName")
                        .HasColumnType("character varying")
                        .HasColumnName("contractorname");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("LoanNumber")
                        .HasColumnType("character varying")
                        .HasColumnName("loannumber");

                    b.Property<bool>("Mortgage")
                        .HasColumnType("boolean")
                        .HasColumnName("mortgage");

                    b.Property<string>("ReferredBy")
                        .HasColumnType("character varying")
                        .HasColumnName("refferedby");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId")
                        .IsUnique();

                    b.ToTable("mortgages");
                });

            modelBuilder.Entity("Domain.Entities.ProfileData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("text")
                        .HasColumnName("addressline1");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<string>("InsuredName")
                        .HasColumnType("text")
                        .HasColumnName("insuredname");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int[]>("Roles")
                        .HasColumnType("integer[]")
                        .HasColumnName("roles");

                    b.Property<Guid?>("State")
                        .HasColumnType("uuid")
                        .HasColumnName("state");

                    b.Property<string>("Zip")
                        .HasColumnType("text")
                        .HasColumnName("zip");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("profiledata");
                });

            modelBuilder.Entity("Domain.Entities.Spouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<Guid>("IntakeFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("intakeformid");

                    b.Property<bool>("IsSpouse")
                        .HasColumnType("boolean")
                        .HasColumnName("isspouse");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("character varying")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("IntakeFormId")
                        .IsUnique();

                    b.ToTable("spouses");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("states");
                });

            modelBuilder.Entity("Domain.Entities.Storm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("storms");
                });

            modelBuilder.Entity("Domain.Entities.TypeOfLoss", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("type_of_loss");
                });

            modelBuilder.Entity("Domain.Entities.TypeOfOccupation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("createdby");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("lastmodified");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("type_of_occupation");
                });

            modelBuilder.Entity("Domain.Entities.Claim", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithOne("Claim")
                        .HasForeignKey("Domain.Entities.Claim", "IntakeFormId")
                        .HasConstraintName("claims_intakeform_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithMany("Documents")
                        .HasForeignKey("IntakeFormId")
                        .HasConstraintName("documents_intakeform_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.Envelope", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithOne("Envelope")
                        .HasForeignKey("Domain.Entities.Envelope", "IntakeFormId")
                        .HasConstraintName("intakeforms_envelope_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.InsuranceAgencyInfo", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithOne("InsuranceAgency")
                        .HasForeignKey("Domain.Entities.InsuranceAgencyInfo", "IntakeFormId")
                        .HasConstraintName("insuranceagencies_intakeform_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.IntakeForm", b =>
                {
                    b.HasOne("Domain.Entities.InsuranceCompany", "InsuranceCompanyInfo")
                        .WithMany()
                        .HasForeignKey("InsuranceCompany")
                        .HasConstraintName("company_intakeforms_fk");

                    b.HasOne("Domain.Entities.Storm", "StormInfo")
                        .WithMany()
                        .HasForeignKey("StormName")
                        .HasConstraintName("storms_intakeforms_fk");

                    b.HasOne("Domain.Entities.TypeOfLoss", "TypeOfLossInfo")
                        .WithMany()
                        .HasForeignKey("TypeOfLoss")
                        .HasConstraintName("loss_intakeforms_fk");

                    b.HasOne("Domain.Entities.TypeOfOccupation", "TypeOfOccupationInfo")
                        .WithMany()
                        .HasForeignKey("TypeOfOccupation")
                        .HasConstraintName("occupation_intakeforms_fk");

                    b.HasOne("Domain.Entities.ProfileData", "User")
                        .WithOne("IntakeForm")
                        .HasForeignKey("Domain.Entities.IntakeForm", "UserId")
                        .HasConstraintName("oprofiledata_intakeforms_fkey")
                        .IsRequired();

                    b.Navigation("InsuranceCompanyInfo");

                    b.Navigation("StormInfo");

                    b.Navigation("TypeOfLossInfo");

                    b.Navigation("TypeOfOccupationInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.MortgageInfo", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithOne("Mortgage")
                        .HasForeignKey("Domain.Entities.MortgageInfo", "IntakeFormId")
                        .HasConstraintName("mortgages_intakeform_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.ProfileData", b =>
                {
                    b.HasOne("Domain.Entities.State", "StateInfo")
                        .WithMany()
                        .HasForeignKey("State")
                        .HasConstraintName("profiledata_fk");

                    b.Navigation("StateInfo");
                });

            modelBuilder.Entity("Domain.Entities.Spouse", b =>
                {
                    b.HasOne("Domain.Entities.IntakeForm", "IntakeForm")
                        .WithOne("Spouse")
                        .HasForeignKey("Domain.Entities.Spouse", "IntakeFormId")
                        .HasConstraintName("spouses_intakeform_fkey")
                        .IsRequired();

                    b.Navigation("IntakeForm");
                });

            modelBuilder.Entity("Domain.Entities.IntakeForm", b =>
                {
                    b.Navigation("Claim");

                    b.Navigation("Documents");

                    b.Navigation("Envelope");

                    b.Navigation("InsuranceAgency");

                    b.Navigation("Mortgage");

                    b.Navigation("Spouse");
                });

            modelBuilder.Entity("Domain.Entities.ProfileData", b =>
                {
                    b.Navigation("IntakeForm");
                });
#pragma warning restore 612, 618
        }
    }
}
