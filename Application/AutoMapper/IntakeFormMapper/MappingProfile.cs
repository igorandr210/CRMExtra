using System.Globalization;
using System.Linq;
using Application.Authorization.DTOs;
using Application.Documents.DTOs;
using Application.IntakeForms.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper.IntakeFormMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IntakeFormRequestDto, Domain.Entities.IntakeForm>()
                .ForMember(x => x.ClaimChecks, opt => opt.MapFrom(x => x.ClaimChecks))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserData))
                .ForMember(x => x.IntakeFormTypesOfLossInfo, opt => opt.MapFrom(x => x.TypesOfLoss.Select(y => new IntakeFormTypeOfLoss { IntakeFormId = x.Id, TypeOfLossId = y.Value })))
                .ReverseMap();
            CreateMap<Domain.Entities.ClaimCheck, ClaimCheckRequestDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
            CreateMap<Domain.Entities.ClaimCheck, ClaimCheckResponseDto>()
              .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
              .ForMember(x => x.FileName, opt => opt.MapFrom(x => x.Document.FileName))
              .ReverseMap();
            CreateMap<Domain.Entities.IntakeForm, IntakeFormResponseDto>()
                .ForMember(x => x.ClaimChecks, opt => opt.MapFrom(x => x.ClaimChecks))
                .ForMember(x => x.UserData, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.TypesOfLoss, opt => opt.MapFrom(x => x.IntakeFormTypesOfLossInfo.Select(y => y.TypeOfLossInfo).ToList()))
                .ReverseMap();
            CreateMap<Domain.Entities.ProfileData, IntakeFormRequestDto>()
                .ForMember(x => x.UserData, opt => opt.MapFrom(x => x))
                .ReverseMap();
            CreateMap<Domain.Entities.IntakeForm, IsSubmitedResponseDTO>()
                .ReverseMap();
            CreateMap<Domain.Entities.Claim, ClaimDto>()
                .ForMember(x => x.IsClaimCheck, opt => opt.MapFrom(x => x.ClaimChecks))
                .ReverseMap();
            CreateMap<Domain.Entities.MortgageInfo, MortgageDto>()
                .ReverseMap();
            CreateMap<Domain.Entities.InsuranceAgencyInfo, InsuranceDto>()
                .ReverseMap();
            CreateMap<Domain.Entities.Spouse, SpouseDto>()
                .ReverseMap();
            CreateMap<Domain.Entities.Document, FileInfoDto>()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.DocumentType, opt => opt.MapFrom(x => x.DocumentType))
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => x.FileName))
                .ReverseMap();
            CreateMap<Domain.Entities.IntakeForm, IntakeFormExcelDto>()
                .ForMember(x => x.IdForYear, opt => opt.MapFrom(x => x.User.IdForYear))
                .ForMember(x => x.InsuredName, opt => opt.MapFrom(x => x.User.InsuredName))
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.User.City))
                .ForMember(x => x.AddressLine1, opt => opt.MapFrom(x => x.User.AddressLine1))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.Zip, opt => opt.MapFrom(x => x.User.Zip))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.User.Phone))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.User.StateInfo.Value))
                .ForMember(x => x.DateOfLoss,
                    opt => opt.MapFrom(x => string.Join(", \n",
                        x.DateOfLoss.Select(d => d.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))))
                .ForMember(x => x.TypeOfLossInfoValue,
                    opt => opt.MapFrom(x => string.Join(", \n",
                        x.IntakeFormTypesOfLossInfo.Select(d => d.TypeOfLossInfo.Value))))
                .ForMember(x => x.InsuranceAgency, opt => opt.MapFrom(x => x.InsuranceAgency.InsuranceAgency))
                .ForMember(x => x.NameOfAgent, opt => opt.MapFrom(x => x.InsuranceAgency.NameOfAgent))
                .ForMember(x => x.AgentPhone, opt => opt.MapFrom(x => x.InsuranceAgency.AgentPhone))
                .ForMember(x => x.AgentEmail, opt => opt.MapFrom(x => x.InsuranceAgency.AgentEmail))
                .ForMember(x => x.Mortgage, opt => opt.MapFrom(x => x.Mortgage.Mortgage))
                .ForMember(x => x.MortgageCompanyname, opt => opt.MapFrom(x => x.Mortgage.Companyname))
                .ForMember(x => x.MortgageContractorEmail, opt => opt.MapFrom(x => x.Mortgage.ContractorEmail))
                .ForMember(x => x.MortgageContractorName, opt => opt.MapFrom(x => x.Mortgage.ContractorName))
                .ForMember(x => x.MortgageLoanNumber, opt => opt.MapFrom(x => x.Mortgage.LoanNumber))
                .ForMember(x => x.MortgageReferredBy, opt => opt.MapFrom(x => x.Mortgage.ReferredBy))
                .ForMember(x => x.ClaimNumber, opt => opt.MapFrom(x => x.Claim.ClaimNumber))
                .ForMember(x => x.ClaimInfo, opt => opt.MapFrom(x => x.Claim.ClaimInfo))
                .ForMember(x => x.ClaimFilledDate, opt => opt.MapFrom(x => x.Claim.ClaimFilledDate));
            CreateMap<TypeOfLoss, TypeOfLossResponseDto>()
                .ForMember(x => x.TypeOfLossId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Value)).ReverseMap();
            CreateMap<IntakeFormTypeOfLoss, TypeOfLossResponseDto>()
                .ForMember(x => x.TypeOfLossId, opt => opt.MapFrom(x => x.TypeOfLossId))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.TypeOfLossInfo.Value)).ReverseMap();
        }
    }
}