using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BoardGamesManagerApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool JsonShouldBeReturned(this HttpContext httpContext)
        {
            var requestContentType = httpContext.Request.Headers["Accept"].FirstOrDefault();
            var requestContentTypeIsEmpty = string.IsNullOrEmpty(requestContentType) || requestContentType == "*/*";

            return requestContentTypeIsEmpty ||
                   requestContentType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}