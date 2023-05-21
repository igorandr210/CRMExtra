using DocuSign.eSign.Model;
using System;

namespace Application.DocuSign.DTOs
{
    public class DocuSignDto
    {
        public ViewUrl ViewUrl { get; set; }
        public Guid EnvelopeId { get; set; }
    }
}
