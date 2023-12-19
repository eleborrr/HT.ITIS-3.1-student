using System.Text.Json;
using System.Text.Json.Serialization;
using Dotnet.Homeworks.Shared.Dto;
using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Minio;
using Minio.DataModel.Args;

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
            var putObjArgs = new PutObjectArgs()
                .WithBucket(_bucketStorageName)
                .WithObject(item.FileName)
                .WithStreamData(item.Content)
                .WithContentType(item.ContentType)
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
        await _minioClient.GetObjectAsync(putObjArgs, cancellationToken);
        return JsonSerializer.Deserialize<Image>(memoryStream);
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

    public Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default)
    {
        var res = new List<string>();
        var listArgs = new ListObjectsArgs()
            .WithBucket(_bucketStorageName);
        var observable = _minioClient.ListObjectsAsync(listArgs);
        var subscription = observable.Subscribe(
                item => res.Add(item.Key),
            ex => Console.WriteLine($"OnError: {ex}"),
            () => Console.WriteLine($"Listed all objects in bucket {_bucketStorageName}\n"));
        return Task.FromResult(res.AsEnumerable());
    }

    public Task<Result> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var objSourceArgs = new CopySourceObjectArgs()
                .WithBucket(_bucketStorageName)
                .WithObject(itemName);

            var copyObjArgs = new CopyObjectArgs()
                .WithBucket(destinationBucketName)
                .WithCopyObjectSource(objSourceArgs);

            _minioClient.CopyObjectAsync(copyObjArgs, cancellationToken);
            return Task.FromResult(new Result(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result(false, e.Message));
        }
    }
}