using System;
using System.Net;

namespace RouteManager.Domain.Core.Extensions;

public class CustomHttpRequestException : Exception
{
    public readonly HttpStatusCode StatusCode;

    public CustomHttpRequestException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}
