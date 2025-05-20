using UFO.Types;
using UFO.Types.Literal;
using Amazon.Lambda;

namespace UFO.DLL.AWS.Lambda;

public class LambdaClient : Literal
{
    public readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly AmazonLambdaConfig _config;
    public readonly AmazonLambdaClient Client;

    public LambdaClient(string url, string accessKey, string secretKey)
        : base(TypeId.Z_CUSTOM)
    {
        _url = url;
        _accessKey = accessKey;
        _secretKey = secretKey;
        _config = new AmazonLambdaConfig
        {
            ServiceURL = url // For local testing with LocalStack or custom endpoints
        };
        Client = new AmazonLambdaClient(accessKey, secretKey, _config);
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write($"LambdaClient{{\"{_url}\", \"{_accessKey}\"}}");
    }
}
