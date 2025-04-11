using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_http_server.src;

public class HttpResponse
{
    public int StatusCode { get; set; } = 200;
    public string StatusMessage { get; set; } = "OK";
    public Dictionary<string, string> Headers { get; set; } = new();
    public string Body { get; set; }

    public byte[] ToByteArray()
    {
        var responseBuilder = new StringBuilder();

        // Status line
        responseBuilder.Append($"HTTP/1.1 {StatusCode} {StatusMessage}\r\n");

        // Add content length header if body exists and no content length header is present
        if (!string.IsNullOrEmpty(Body) && !Headers.ContainsKey("Content-Length"))
        {
            Headers["Content-Length"] = Body.Length.ToString();
        }

        // Headers
        foreach (var header in Headers)
        {
            responseBuilder.Append($"{header.Key}: {header.Value}\r\n");
        }

        // Empty line separating headers from body
        responseBuilder.Append("\r\n");

        // Body
        if (!string.IsNullOrEmpty(Body))
        {
            responseBuilder.Append(Body);
        }

        return Encoding.UTF8.GetBytes(responseBuilder.ToString());
    }
}
