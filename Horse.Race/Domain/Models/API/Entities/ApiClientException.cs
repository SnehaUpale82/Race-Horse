using System.Net;

namespace Domain.Models.API.Entities;

public class ApiClientException : Exception
{
    public HttpStatusCode TheStatusCode { get; set; }

    public string? ThePath { get; set; }

    public Type TheType { get; set; }

    public string TheErrorContent { get; set; }
}