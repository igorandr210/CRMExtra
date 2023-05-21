using System;

namespace Application.IntakeForms.DTOs
{
    public class SpouseDto
    {
        public Guid Id { get; set; }
        public bool IsSpouse { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}