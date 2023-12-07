using MediatR;
using Microsoft.Extensions.Logging;

namespace GamersWorld.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(
       TRequest request
       , RequestHandlerDelegate<TResponse> next
       , CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation($"Incoming request detail is {request}", requestName);
        return await next();
    }
}