using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand: ICommand<Guid>, ICommand //TODO: Inherit certain interface 
{
    public Guid Guid { get; init; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}