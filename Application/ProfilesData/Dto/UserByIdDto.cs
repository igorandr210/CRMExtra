using System;
using System.Collections.Generic;

namespace Application.ProfilesData.Dto
{
    public class UserByIdDto
    {
        public Guid UserId { get; set; }
        public string InsuredName { get; set; }
        public DateTime[] DatesOfLoss { get; set; }
        public List<string> TypesOfLoss { get; set; }
        public string ProfileId { get; set; }
    }
}
