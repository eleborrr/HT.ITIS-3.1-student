using Minio;
using Minio.DataModel.Args;
using MinioConfig = Dotnet.Homeworks.Storage.API.Configuration.MinioConfig;

namespace Dotnet.Homeworks.Storage.API.ServicesExtensions;

public static class AddMinioExtensions
{
    public static IServiceCollection AddMinioClient(this IServiceCollection services,
        MinioConfig minioConfiguration)
    {
        var minioClient = new MinioClient().WithEndpoint(minioConfiguration.Endpoint)
            .WithCredentials(minioConfiguration.Username, minioConfiguration.Password)
            .Build();

        
        
        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}