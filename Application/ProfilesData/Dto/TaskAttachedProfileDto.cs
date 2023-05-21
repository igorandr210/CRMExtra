using System;
using Domain.Entities;
using Domain.Enum;

namespace Application.ProfilesData.Dto
{
    public class TaskAttachedProfileDto
    {
        public Guid Id { get; set; }
        public string InsuredName { get; set; }
        public string ProfileId { get; set; }
        public IntakeFormStatus IntakeFormStatus { get; set; }
        public TypeOfLoss TypeOfLoss { get; set; }
    }
}