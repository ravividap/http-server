namespace codecrafters_http_server.src;

public class FilesHandler(string directory) : IRequestHandler
{
    public HttpResponse HandleRequest(HttpRequest request)
    {
        string fileName = request.Path.Contains("/files/")
          ? request.Path.Substring(request.Path.IndexOf("/files/") + 7)
          : string.Empty;

        Console.WriteLine($"file: {fileName}");


        string fullPath = Path.Combine(directory, fileName);

        if(request.Method == "GET")
        {
            if (File.Exists(fullPath))
            {
                var contents = File.ReadAllText(fullPath);
                if (contents != null)
                {
                    return new HttpResponse
                    {
                        StatusCode = 200,
                        StatusMessage = "OK",
                        Headers = { ["Content-Type"] = "application/octet-stream" },
                        Body = contents
                    };
                }
            }
        }

        if(request.Method == "POST")
        {
            File.WriteAllText(fullPath, request.Body);

            return new HttpResponse
            {
                StatusCode = 201,
                StatusMessage = "Created"
            };
        }
        

        return new HttpResponse
        {
            StatusCode = 404,
            StatusMessage = "Not Found"
        };
    }
}