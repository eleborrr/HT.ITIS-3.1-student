using Dotnet.Homeworks.Shared.Dto;
using MediatR;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommand: IRequest<Result>  //TODO: Inherit certain interface 
{
}

public interface ICommand<TResponse>: IRequest<Result<TResponse>> //TODO: Inherit certain interface 
{
}