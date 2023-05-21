using System;

namespace Application.DropDownValues.DTOs
{
    public class DropDownValue<T>
    {
        public Guid? Id { get; set; }
        public T Value { get; set; }
    }
}