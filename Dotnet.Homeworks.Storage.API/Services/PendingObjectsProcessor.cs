using Minio;

namespace Dotnet.Homeworks.Storage.API.Services;

public class PendingObjectsProcessor : BackgroundService
{
    private readonly IStorageFactory _storageFactory;

    public PendingObjectsProcessor(IMinioClient minioClient, IStorageFactory storageFactory)
    {
        _storageFactory = storageFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var storage = await _storageFactory.CreateImageStorageWithinBucketAsync(Constants.Buckets.Pending);
        var pendingFiles = await storage.EnumerateItemNamesAsync(stoppingToken);
        foreach (var pendingFile in pendingFiles)
        {
            var fileData = await storage.GetItemAsync(pendingFile, stoppingToken);

            try
            {
                await storage.CopyItemToBucketAsync(pendingFile, fileData.Metadata[Constants.MetadataKeys.Destination],
                    stoppingToken);
            }
            catch (Exception e)
            {
                
            }

            await storage.RemoveItemAsync(pendingFile, stoppingToken);
            
        }
        await Task.Delay(Constants.PendingObjectProcessor.Period, stoppingToken);
    }
}