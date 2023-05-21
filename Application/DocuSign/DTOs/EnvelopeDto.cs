using System;

namespace Application.DocuSign.DTOs
{
    public class EnvelopeDto
    {
        public string Event { get; set; }

        public EnvelopeDataDto Data { get; set; }
    }
}
