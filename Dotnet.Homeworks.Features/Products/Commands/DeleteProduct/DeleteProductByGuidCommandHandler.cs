using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductByGuidCommandHandler: ICommandHandler<DeleteProductByGuidCommand> //TODO: Inherit certain interface 
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductByGuidCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductByGuidCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.GetProductRepository().DeleteProductByGuidAsync(request.Guid, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (ArgumentException e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
    }
}