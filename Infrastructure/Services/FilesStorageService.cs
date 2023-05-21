using Amazon.S3;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Application.Common.Enums;
using System.IO;
using System;
using System.Net;
using Application.Documents.DTOs;
using AutoMapper;

namespace Infrastructure.Services
{
    public abstract class FilesStorageServiceBase : IStorageService
    {
        private readonly AmazonS3Client _client;
        private readonly IMapper _mapper;

        public abstract BucketType Type { get; }

        protected FilesStorageServiceBase(AmazonS3Client client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<string> UploadFile(string key, IFormFile file)
        {
            await _client.PutObjectAsync(new PutObjectRequest()
            {
                BucketName = Type.ToString().ToLower(),
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            });

            return key;
        }

        public async Task<string> UploadFile(string key, Stream file)
        {
            await _client.PutObjectAsync(new PutObjectRequest()
            {
                BucketName = Type.ToString().ToLower(),
                Key = key,
                InputStream = file,
            });

            return key;
        }

        public async Task<bool> DeleteFile(string key)
        {
            await _client.DeleteObjectAsync(new DeleteObjectRequest
            {
                Key = key,
                BucketName = Type.ToString().ToLower(),
            });

            return true;
        }

        public Task<string> GetFileLink(string key)
        {
            var res = _client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = Type.ToString().ToLower(),
                Key = key,
                Expires = DateTime.Now.AddHours(2),
            });

            return Task.FromResult(res);
        }

        public async Task<DownloadFileDto> DownloadFile(string key)
        {
            var response = await _client.GetObjectAsync(Type.ToString().ToLower(), key);
       
            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new FileNotFoundException($"The document '{key}' is not found");
            }

            return _mapper.Map<DownloadFileDto>(response);
        }
    }
}