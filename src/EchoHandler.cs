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
            var encodings = request.Headers["accept-encoding"].Split(", ");

             useCompression = encodings.Contains("gzip") ? true : false;

            Console.WriteLine($"use compre: {useCompression}");
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
            Body = content,
            UseCompression = useCompression
        };
    }
}
