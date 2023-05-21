using System;
using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.Documents.DTOs
{
    public class FileUploadRequestDto
    {
        public IFormFile File { get; set; }
        public DocumentType Type { get; set; }
    }
}