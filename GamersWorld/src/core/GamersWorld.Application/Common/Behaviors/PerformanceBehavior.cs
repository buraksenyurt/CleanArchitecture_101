using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GamersWorld.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger = logger;
    private readonly Stopwatch _observer = new();
    public async Task<TResponse> Handle(
        TRequest request
        , RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        _observer.Start();
        var response = await next();
        _observer.Stop();

        var duration = _observer.ElapsedMilliseconds;
        if (duration < 250) //TODO@buraksenyurt 250 değeri de dışarıdan içeriye enjekte edilen bir metrik olmalı
            return response;

        _logger.LogWarning($"Overtimed process. {typeof(TRequest).Name}");

        return response;
    }
}
