using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;
using Exception = System.Exception;

namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

internal sealed class InsertProductCommandHandler : ICommandHandler<InsertProductCommand, InsertProductDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public InsertProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsertProductDto>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _unitOfWork.ProductRepository.InsertProductAsync(new Product { Name = request.Name },
                cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result<InsertProductDto>(new InsertProductDto(id), true);
        }
        catch (Exception e)
        {
            return new Result<InsertProductDto>(null, false, e.Message);
        }
    }
}