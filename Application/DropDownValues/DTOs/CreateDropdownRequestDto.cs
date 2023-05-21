using Application.Common.Enums;

namespace Application.DropDownValues.DTOs
{
    public class CreateDropdownRequestDto
    {
        public DropDownValue<string> DropDownValue { get; set; }
        public DropDownType Type { get; set; }
    }
}