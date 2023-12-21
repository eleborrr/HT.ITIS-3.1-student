using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public class GetAllUsersQueryHandler: IQueryHandler<GetAllUsersQuery, GetAllUsersDto> //TODO: Inherit certain interface 
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _unitOfWork.UserRepository.GetUsersAsync(cancellationToken);
            var result = users.Select(u => new GetUserDto(u.Id, u.Name, u.Email)).AsEnumerable();
            return new Result<GetAllUsersDto>(new GetAllUsersDto(result), true);
        }
        catch (Exception e)
        {
            return new Result<GetAllUsersDto>(null, false, e.Message);
        }
    }
}