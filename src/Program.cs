using System.Net;

namespace codecrafters_http_server.src;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("HTTP Server starting...");

        var server = new HttpServer(IPAddress.Any, 4221);

        // Configure routes
        server.AddRoute("/", new RootHandler());
        server.AddRoute("/echo", new EchoHandler());
        server.AddRoute("/user-agent", new UserAgentHandler());

        // Start the server
        server.Start();
    }
}
