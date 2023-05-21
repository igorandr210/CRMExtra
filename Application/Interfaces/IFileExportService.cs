using System.Collections.Generic;
using Application.Documents.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IFileExportService<T>
    {
        DownloadFileDto Export(T entity, string fileName);
        DownloadFileDto Export(IEnumerable<T> entities, string fileName);

    }
}
