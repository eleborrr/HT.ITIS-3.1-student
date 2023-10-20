using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.DataAccess.Repositories;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext db = new AppDbContext();
    private ProductRepository? _productRepository;
    public async Task SaveChangesAsync(CancellationToken token)
    {
        await db.SaveChangesAsync(token);
    }

    public IProductRepository GetProductRepository()
    {
        return _productRepository ??= new ProductRepository(db);
    }
}