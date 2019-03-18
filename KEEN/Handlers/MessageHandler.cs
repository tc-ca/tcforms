using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace KEEN.Handlers
{
    public abstract class MessageHandler : DelegatingHandler
    {
        protected ILog Logger => LogManager.GetLogger(GetType());

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString();
            var requestInfo = $"{request.Method} {request.RequestUri}";

            //get correlationId from headers.
            if (request.Headers.TryGetValues("X-Correlation-ID", out var headerValues))
            {
                correlationId = headerValues.FirstOrDefault();
            }

            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            await IncommingMessageAsync(correlationId, requestInfo, requestMessage);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode && response.Content != null)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(correlationId, requestInfo, responseMessage);

            return response;
        }
        protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message);
        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);
    }

    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                Logger.Info($"{correlationId} - Request: {requestInfo}\r\n{Encoding.UTF8.GetString(message)}");
            });
        }


        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                Logger.Info($"{correlationId} - Request: {requestInfo}\r\n{Encoding.UTF8.GetString(message)}");
            });
        }
    }
}