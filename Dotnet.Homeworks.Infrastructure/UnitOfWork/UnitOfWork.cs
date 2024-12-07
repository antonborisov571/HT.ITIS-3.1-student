using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(AppDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        ProductRepository = productRepository;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _dbContext.SaveChangesAsync(token);
    }
}