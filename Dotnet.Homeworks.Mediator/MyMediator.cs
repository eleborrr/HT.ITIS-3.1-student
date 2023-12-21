using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Mediator;

public class MyMediator: IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public MyMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handler = _serviceProvider.GetService<IRequestHandler<IRequest<TResponse>, TResponse>>();
        return handler.Handle(request, cancellationToken);
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var handler = _serviceProvider.GetService<IRequestHandler<TRequest>>();
        return handler.Handle(request, cancellationToken);
    }

    public Task<dynamic?> Send(dynamic request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}