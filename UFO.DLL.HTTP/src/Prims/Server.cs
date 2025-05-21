using System.Net;
using System.Text;
using System.Text.Json;
using UFO.Types.Literal;

namespace UFO.DLL.HTTP;

public class HttpServer : Literal
{
    private readonly HttpListener _listener = new();
    private readonly Thread _serverThread;

    public HttpServer(string urlPrefix="http://localhost:8080/")
        : base(Types.TypeId.Z_CUSTOM)
    {
        _listener.Prefixes.Add(urlPrefix);
        _serverThread = new Thread(StartListening);
    }

    public void Start() => _serverThread.Start();

    private void StartListening()
    {
        _listener.Start();
        while (true)
        {
            var context = _listener.GetContext();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                HandleRequest(context);
            });
        }
    }

    private void HandleRequest(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        // Example: parse JSON input
        using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
        var body = reader.ReadToEnd();
        var result = EvaluateUFO(body); // You call your interpreter here

        var responseString = JsonSerializer.Serialize(new { result });
        var buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentType = "application/json";
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.Close();
    }

    private object EvaluateUFO(string input)
    {
        // Call your UFO interpreter on `input` and return result
        return $"Evaluated: {input}";
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write($"HTTPServer{{}}");
    }
}
