using Application.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Application.Common.Enums;
using System;

namespace Application.Common.Factories
{
    public delegate IStorageService BucketServiceFactory(BucketType bucketType);

    public class FileServiceFactory
    {
        private readonly IEnumerable<IStorageService> _storageServices;

        public FileServiceFactory(IEnumerable<IStorageService> storageServices)
        {
            _storageServices = storageServices;
        }

        public BucketServiceFactory GetFileServiceDeligate()
        {
            return (type) =>
            {
                var instance = _storageServices.FirstOrDefault(x=>x.Type == type);

                if (instance == null)
                {
                    throw new Exception($"service for {type} are not found");
                }

                return instance;
            };
        }
    }
}
