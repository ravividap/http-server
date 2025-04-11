namespace codecrafters_http_server.src;

public interface IRequestHandler
{
    HttpResponse HandleRequest(HttpRequest request);
}
