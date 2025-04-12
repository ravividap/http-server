namespace codecrafters_http_server.src;

public class EchoHandler : IRequestHandler
{
    public HttpResponse HandleRequest(HttpRequest request)
    {
        // Extract content to echo from the path
        string content = request.Path.Contains("/echo/")
            ? request.Path.Substring(request.Path.IndexOf("/echo/") + 6)
            : string.Empty;

        var useCompression = false;
        if (request.Headers.ContainsKey("accept-encoding"))
        {
            useCompression = request.Headers["accept-encoding"] == "gzip" ? true : false;

            Console.WriteLine($"use comp: {useCompression}");
        }


        return new HttpResponse
        {
            StatusCode = 200,
            StatusMessage = "OK",
            Headers = { ["Content-Type"] = "text/plain" },
            Body = content,
            UseCompression = useCompression
        };
    }
}
