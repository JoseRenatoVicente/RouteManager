using System;
using System.Net;
using System.Runtime.Serialization;

namespace RouteManager.Domain.Core.Extensions;

[Serializable]
public sealed class CustomHttpRequestException : Exception
{
    public readonly HttpStatusCode StatusCode;

    public CustomHttpRequestException()
    {
    }
    public CustomHttpRequestException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    private CustomHttpRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
