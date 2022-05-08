using Dawn;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CloudFileSystem.Application.Behaviours;

/// <summary>
/// Checks a call's performance by calculating its execution speed and logging in case it is higher
/// than 500ms.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}"/>
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// The timer
    /// </summary>
    private readonly Stopwatch _timer;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<TRequest> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUserService">The current user service.</param>
    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
    }

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next"/>
    /// delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">
    /// Awaitable delegate for the next action in the pipeline. Eventually this delegate represents
    /// the handler.
    /// </param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse"/></returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        _logger.LogDebug("Request {@Request} lasted {ElapsedMilliseconds} milliseconds", request, _timer.ElapsedMilliseconds);

        return response;
    }
}