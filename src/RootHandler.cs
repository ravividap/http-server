namespace codecrafters_http_server.src;

public class RootHandler : IRequestHandler
{
    public HttpResponse HandleRequest(HttpRequest request)
    {
        return new HttpResponse
        {
            StatusCode = 200,
            StatusMessage = "OK"
        };
    }
}
