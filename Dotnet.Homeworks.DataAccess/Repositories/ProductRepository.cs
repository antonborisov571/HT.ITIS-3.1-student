using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Products.ToListAsync(cancellationToken);
    }

    public async Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
        }
    }

    public Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Update(product);
        return Task.CompletedTask;
    }

    public async Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        var entry = await _dbContext.Products.AddAsync(product, cancellationToken);
        return entry.Entity.Id;
    }
}