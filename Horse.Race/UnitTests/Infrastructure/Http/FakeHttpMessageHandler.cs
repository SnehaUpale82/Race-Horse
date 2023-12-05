namespace UnitTests.Infrastructure.Http;

public class FakeHttpMessageHandler : HttpMessageHandler 
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return SendAsyncHandler(request, cancellationToken);
    }

    public virtual Task<HttpResponseMessage> SendAsyncHandler(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage());
    }
}
