using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.DataAccess.Repositories;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext _db;
    public IProductRepository ProductRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        UserRepository = new UserRepository(_db);
        ProductRepository = new ProductRepository(_db);
    }
    
    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _db.SaveChangesAsync(token);
    }
}