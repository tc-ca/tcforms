using System.Net;

namespace TCWebAPI
{
    public class WebApiResponse<T>
    {
        public string CorrelationId { get; set; }
        public T Response { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
        public bool IsError { get; internal set; }
    }
}
