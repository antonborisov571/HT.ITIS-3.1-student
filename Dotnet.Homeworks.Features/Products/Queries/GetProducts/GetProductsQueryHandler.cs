using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsAsync(cancellationToken);
            var productsDto = products.Select(x => new GetProductDto(x.Id, x.Name));
            return new Result<GetProductsDto>(new GetProductsDto(productsDto), true);
        }
        catch (Exception e)
        {
            return new Result<GetProductsDto>(null, false, e.Message);
        }
    }
}