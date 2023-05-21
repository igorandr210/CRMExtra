using System.IO;

namespace Application.Documents.DTOs
{
    public class DownloadFileDto
    {
        public Stream Stream { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
    }
}
