using System.IO.Compression;
using System.Text;

namespace codecrafters_http_server.src;

public static class CompressionHelper
{
    public static byte[] CompressWithGzip(string data)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                using (var writer = new StreamWriter(gzipStream, Encoding.UTF8))
                {
                    writer.Write(data);
                }
            }

            return memoryStream.ToArray();
        }
    }

    public static bool ClientSupportsGzip(HttpRequest request)
    {
        if (request.Headers.TryGetValue("Accept-Encoding", out string encodings))
        {
            return encodings.Split(',')
                .Select(e => e.Trim().ToLowerInvariant())
                .Contains("gzip");
        }
        return false;
    }
}
