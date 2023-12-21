using System.Reactive.Linq;
using Dotnet.Homeworks.Shared.Dto;
using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Dotnet.Homeworks.Storage.API.Services;

public class ImageStorage : IStorage<Image>
{
    private readonly string _bucketStorageName;
    private readonly IMinioClient _minioClient;

    public ImageStorage(string bucketStorageName, IMinioClient minioClient)
    {
        _bucketStorageName = bucketStorageName;
        _minioClient = minioClient;
    }

    public async Task<Result> PutItemAsync(Image item, CancellationToken cancellationToken = default)
    {
        try
        {
            item.Content.Position = 0;
            item.Metadata.Add(Constants.MetadataKeys.Destination, _bucketStorageName);
            var putObjArgs = new PutObjectArgs()
                .WithBucket(Constants.Buckets.Pending)
                .WithObject(item.FileName)
                .WithStreamData(item.Content)
                .WithContentType(item.ContentType)
                .WithHeaders(item.Metadata)
                .WithObjectSize(item.Content.Length);
            await _minioClient.PutObjectAsync(putObjArgs, cancellationToken);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }

    public async Task<Image?> GetItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();   

        var putObjArgs = new GetObjectArgs()
            .WithBucket(_bucketStorageName)
            .WithObject(itemName)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });
        ObjectStat resp;
        try
        {
            resp = await _minioClient.GetObjectAsync(putObjArgs, cancellationToken);
        }
        catch (BucketNotFoundException)
        {
            return null;
        }
        catch (ObjectNotFoundException)
        {
            return null;
        }
        memoryStream.Position = 0;
        return new Image(memoryStream, resp.ObjectName, resp.ContentType, resp.MetaData);
    }

    public Task<Result> RemoveItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        try
        {
            var removeObjArgs = new RemoveObjectArgs()
                .WithBucket(_bucketStorageName)
                .WithObject(itemName);
            _minioClient.RemoveObjectAsync(removeObjArgs, cancellationToken);
            return Task.FromResult(new Result(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result(false, e.Message));
        }
    }

    public async Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default)
    {
        var listArgs = new ListObjectsArgs()
            .WithBucket(_bucketStorageName);
        return await _minioClient.ListObjectsAsync(listArgs, cancellationToken)
            .Select(x => x.Key).ToList();;
    }

    public async Task<Result> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var objSourceArgs = new CopySourceObjectArgs()
                .WithBucket(_bucketStorageName)
                .WithObject(itemName);

            var copyObjArgs = new CopyObjectArgs()
                .WithBucket(destinationBucketName)
                .WithObject(itemName)
                .WithCopyObjectSource(objSourceArgs);

            await _minioClient.CopyObjectAsync(copyObjArgs, cancellationToken);
            return new Result(true);
        }
        catch (BucketNotFoundException e)
        {
            return new Result(false, e.Message);
        }
        catch (ObjectNotFoundException e)
        {
            return new Result(false,e.Message);
        }
    }
}