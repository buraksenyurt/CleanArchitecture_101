using MediatR;
using Microsoft.Extensions.Logging;

namespace GamersWorld.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request
        , RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception excp)
        {
            _logger.LogError(excp, "Unhandled exception detected!");
            throw;
        }
    }
}
