using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler: IQueryHandler<GetProductsQuery, GetProductsDto> //TODO: Inherit certain interface 
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = (await _unitOfWork.ProductRepository.GetAllProductsAsync(cancellationToken))
            .Select(product => new GetProductDto(product.Id, product.Name));
        
        return new Result<GetProductsDto>(
            new GetProductsDto(products), true, null);    }
}