namespace Dotnet.Homeworks.Mediator;

public class MyMediator: IMediator
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        throw new NotImplementedException();
    }

    public Task<dynamic?> Send(dynamic request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}