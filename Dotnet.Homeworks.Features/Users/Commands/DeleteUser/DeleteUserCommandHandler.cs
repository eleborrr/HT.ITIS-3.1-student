using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler: ICommandHandler<DeleteUserCommand> //TODO: Inherit certain interface 
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //TODO ваще нет проверок на ошибки лол)
        await _unitOfWork.UserRepository.DeleteUserByGuidAsync(request.Guid, cancellationToken);
        return new Result(true);
    }
}