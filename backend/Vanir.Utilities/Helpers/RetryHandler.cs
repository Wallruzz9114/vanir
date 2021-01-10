using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;

namespace Vanir.Utilities.Helpers
{
    public class RetryHandler : DelegatingHandler
    {
        private readonly ILogger<RetryHandler> _logger;
        private HttpRequestMessage _requestMessage;
        private CancellationToken _cancellationToken;

        public RetryHandler(ILogger<RetryHandler> logger) => _logger = logger;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            _requestMessage = requestMessage;
            _cancellationToken = cancellationToken;

            var context = new Context { { "retryCount", 0 } };

            return Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(5, (n) => TimeSpan.FromSeconds(n), LoginFailure)
                .ExecuteAsync(() => ((Func<Context, Task<HttpResponseMessage>>)ExecuteAsync)(context));
        }

        private async Task<HttpResponseMessage> ExecuteAsync(Context context)
        {
            if (context.TryGetValue("retryCount", out var retryObject) && retryObject is int retries)
            {
                retries++;
                context["retryCount"] = retries;
            }

            var response = await base.SendAsync(_requestMessage, _cancellationToken);
            response.EnsureSuccessStatusCode();
            response.Headers.Add("X-Retry-Count", $"{ context["retryCount"] }");

            return response;
        }

        private void LoginFailure(Exception exception, TimeSpan waitTime, int retryCount, Context context)
        {
            _logger.LogWarning("Retrying again after { waitTime } - count: { retryCount }", waitTime, retryCount);
        }
    }
}