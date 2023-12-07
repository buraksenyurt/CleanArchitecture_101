using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace GamersWorld.Application.Common.Behaviors;

public class LoggingBehavior<TRequest>(ILogger<TRequest> logger)
    : IRequestPreProcessor<TRequest>
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation($"Incoming request detail is {request}", requestName);
    }
}