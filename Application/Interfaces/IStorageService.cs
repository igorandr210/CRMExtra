using Application.Common.Enums;
using Application.Documents.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStorageService
    {
        BucketType Type { get; }
        Task<string> UploadFile(string key, IFormFile file);
        Task<string> UploadFile(string key, Stream file);
        Task<bool> DeleteFile(string key);
        Task<string> GetFileLink(string key);
        Task<DownloadFileDto> DownloadFile(string key);
    }
}
