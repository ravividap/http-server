using System.Reflection.PortableExecutable;

namespace codecrafters_http_server.src;

public class UserAgentHandler : IRequestHandler
{
    // Handler for the /user-agent path

    public HttpResponse HandleRequest(HttpRequest request)
    {
        string userAgent = string.Empty;

        if (request.Headers.TryGetValue("User-Agent", out string value))
        {
            userAgent = value;
        }

        var headers = new Dictionary<string, string> { ["Content-Type"] = "text/plain" };
        
        if (request.Headers.ContainsKey("Connection"))
        {
            headers.Add("Connection", "close");
        } 

        return new HttpResponse
        {
            StatusCode = 200,
            StatusMessage = "OK",
            Headers = headers,
            Body = userAgent
        };
    }
}