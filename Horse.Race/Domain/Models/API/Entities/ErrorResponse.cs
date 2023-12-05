using System.Net;

namespace Domain.Models.API.Entities;

public class ErrorResponse
{
    public HttpStatusCode HttpStatusCode { get; init; }

    public IEnumerable<string>? RequestErrors { get; init; } = new List<string>();
}