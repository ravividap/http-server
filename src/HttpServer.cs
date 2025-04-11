using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_http_server.src;

public class HttpServer
{
    private readonly TcpListener _listener;
    private readonly Dictionary<string, IRequestHandler> _routes;
    private bool _isRunning;

    public HttpServer(IPAddress ipAddress, int port)
    {
        _listener = new TcpListener(ipAddress, port);
        _routes = new Dictionary<string, IRequestHandler>();
    }

    public void AddRoute(string path, IRequestHandler handler)
    {
        _routes[path] = handler;
    }

    public void Start()
    {
        _isRunning = true;
        _listener.Start();
        Console.WriteLine($"Server started and listening on port {((IPEndPoint)_listener.LocalEndpoint).Port}");

        try
        {
            while (_isRunning)
            {
                // Wait for a client to connect
                var client = _listener.AcceptSocket();
               Task.Run(() => ProcessClientRequest(client));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            Stop();
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _listener.Stop();
        Console.WriteLine("Server stopped");
    }

    private void ProcessClientRequest(Socket client)
    {
        try
        {
            using (client)
            {
                while (client.Connected)
                {
                    var request = HttpRequest.Parse(client);
                    if (request == null) break;

                    var response = RouteRequest(request);
                    SendResponse(client, response);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing request: {ex.Message}");
        }
    }

    private HttpResponse RouteRequest(HttpRequest request)
    {
        // Extract the base path from the request path
        string path = request.Path.Split('/', 3)[1];
        string basePath = "/" + path;

        // Find the appropriate handler
        if (_routes.TryGetValue(basePath, out var handler))
        {
            return handler.HandleRequest(request);
        }

        // Return 404 if no handler is found
        return new HttpResponse
        {
            StatusCode = 404,
            StatusMessage = "Not Found"
        };
    }

    private void SendResponse(Socket client, HttpResponse response)
    {
        byte[] responseBytes = response.ToByteArray();
        client.Send(responseBytes, SocketFlags.None);
    }
}
