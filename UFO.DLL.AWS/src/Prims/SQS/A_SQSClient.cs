using UFO.Types;
using UFO.Types.Literal;
using Amazon.SQS;

namespace UFO.DLL.AWS.SQS;

public class SQSClient : Literal
{
    public readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly AmazonSQSConfig _config;
    public readonly AmazonSQSClient Client;

    public SQSClient(string url, string accessKey, string secretKey)
        : base(TypeId.Z_CUSTOM)
    {
        _url = url;
        _accessKey = accessKey;
        _secretKey = secretKey;
        _config = new AmazonSQSConfig
        {
            ServiceURL = url,
            UseHttp = true // often true for local testing; remove if using HTTPS
        };
        Client = new AmazonSQSClient(accessKey, secretKey, _config);
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write($"SQSClient{{\"{_url}\", \"{_accessKey}\"}}");
    }
}
