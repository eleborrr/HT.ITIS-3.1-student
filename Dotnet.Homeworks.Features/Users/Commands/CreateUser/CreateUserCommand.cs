using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserCommand: ICommand<Guid> //TODO: Inherit certain interface 
{
    public string Name { get; }
    public string Email { get; }

    public CreateUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}