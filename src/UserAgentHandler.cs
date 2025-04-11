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

        return new HttpResponse
        {
            StatusCode = 200,
            StatusMessage = "OK",
            Headers = { ["Content-Type"] = "text/plain" },
            Body = userAgent
        };
    }
}