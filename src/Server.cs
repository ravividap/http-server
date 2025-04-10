using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();
var client = server.AcceptSocket(); // wait for client

var buffer = new byte[1024];

while (client.Connected)
{

    var bytes = client.Receive(buffer, SocketFlags.None);

    var message = Encoding.UTF8.GetString(buffer);

    var request = message.Split("\r\n")[0];

    var path = request.Split("/")[1];

    path = path.Split(" ")[0];
    if(path != "")
    {
        Console.WriteLine($"Path: {path}");
        client.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
    }
    else
    {
        client.Send(Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n\r\n"));
    }

}