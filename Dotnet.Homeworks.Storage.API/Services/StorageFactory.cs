﻿using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Minio;

namespace Dotnet.Homeworks.Storage.API.Services;

public class StorageFactory : IStorageFactory
{
    private readonly IMinioClient _minioClient;

    public StorageFactory(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public Task<IStorage<Image>> CreateImageStorageWithinBucketAsync(string bucketName)
    {
        // TODO: implement creation of IImageStorage with the given bucketName
        // e.g. each storage should work only within its bucket (but still may copy items to another bucket)
        return Task.FromResult<IStorage<Image>>(new ImageStorage(bucketName, _minioClient));
    }
}