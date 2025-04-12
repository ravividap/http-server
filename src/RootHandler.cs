using System.Reflection.PortableExecutable;

namespace codecrafters_http_server.src;

public class RootHandler : IRequestHandler
{
    public HttpResponse HandleRequest(HttpRequest request)
    {
        var headers = new Dictionary<string, string>();

        if (request.Headers.ContainsKey("Connection"))
        {
            headers.Add("Connection", "close");

        }
        return new HttpResponse
        {
            StatusCode = 200,
            StatusMessage = "OK",
            Headers = headers
        };
    }
}
