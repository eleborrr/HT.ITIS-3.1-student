using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Products.AsEnumerable());
    }

    public Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = _dbContext.Products.FirstOrDefault(pr => pr.Id == id);
        if (product is null)
            return Task.FromException(new ArgumentException($"Product with id {id} not found"));
        _dbContext.Products.Remove(product);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var productDb = _dbContext.Products.FirstOrDefault(pr => product.Id == pr.Id);
        productDb!.Name = product.Name;
        return Task.FromResult(_dbContext.Update(productDb));
    }

    public async Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
        return product.Id; //TODO check what id should i return: from Entity or from db
    }
}