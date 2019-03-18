using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace KEEN.Extensions
{
    public static class RequestExtensions
    {
        public static IList<string> ParseAcceptLanguageInfo(this HttpRequestHeaders headers)
            => headers?.AcceptLanguage.OrderByDescending(l => l.Quality).Select(l => l.Value).ToList();
    }
}