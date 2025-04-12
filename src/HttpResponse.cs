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

    public bool UseCompression { get; set; }

    public byte[] ToByteArray()
    {
        byte[] bodyBytes = null;

        if (!string.IsNullOrEmpty(Body))
        {
            if (UseCompression)
            {
                // Compress the body
                bodyBytes = CompressionHelper.CompressWithGzip(Body);
                Body = Encoding.UTF8.GetString(bodyBytes);
                Headers["Content-Encoding"] = "gzip";
                Headers["Content-Length"] = bodyBytes.Length.ToString();
                Headers["Vary"] = "Accept-Encoding";  // Best practice with content negotiation
            }
            else
            {
                bodyBytes = Encoding.UTF8.GetBytes(Body);
                Headers["Content-Length"] = bodyBytes.Length.ToString();
            }
        }


        var responseBuilder = new StringBuilder();

        // Status line
        responseBuilder.Append($"HTTP/1.1 {StatusCode} {StatusMessage}\r\n");

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
