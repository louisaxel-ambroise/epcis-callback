using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace FasTnT.Epcis.Callback.Api.Extensions;

static class HttpRequestExtensions
{
    public static bool HasContentType(this HttpRequest request, string suffix)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (!MediaTypeHeaderValue.TryParse(request.ContentType, out var mt))
        {
            return false;
        }

        // Matches +json, e.g. application/ld+json
        if (mt.MediaType.Contains(suffix, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }
}
