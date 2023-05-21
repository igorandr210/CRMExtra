using System;

namespace Application.Authorization.DTOs
{
    public class SignUpDto
    {
        public string Email { get; set; }
        public string InsuredName { get; set; }
        public string AddressLine1 { get; set; }
        public string Phone { get; set; }
        public Guid? State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string ProfileId { get; set; }
    }
}