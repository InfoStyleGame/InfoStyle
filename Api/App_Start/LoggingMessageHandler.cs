using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Api
{
    public class LoggingMessageHandler : DelegatingHandler
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LoggingMessageHandler).Name);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString();

            log.InfoFormat("Call [{0}]: {1} {2}", correlationId, request.Method, request.RequestUri);

            var stopwatch = Stopwatch.StartNew();

            var response = await base.SendAsync(request, cancellationToken);

            log.InfoFormat("Completed [{0}]: {1} {2} - {3} ({4} ms)",
                correlationId,
                request.Method,
                request.RequestUri,
                response.StatusCode,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}
