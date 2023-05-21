using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enum;

namespace Application.Authorization.DTOs
{
    public class ProfileInfoDto
    {
        public Guid? UserId { get; set; }
        public string ProfileId { get; set; }
        public string FullName { get; set; }
        public DateTime[] DatesOfLoss { get; set; }
        public List<string> TypesOfLoss { get; set; }
        public IntakeFormStatus Status { get; set; }
    }
}