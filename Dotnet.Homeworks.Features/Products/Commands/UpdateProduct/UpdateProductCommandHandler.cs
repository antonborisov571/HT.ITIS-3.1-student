using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.ProductRepository.UpdateProductAsync(new Product 
                { Id = request.Guid, Name = request.Name }, 
                cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }
}