using UFO.Types;
using UFO.Types.Literal;
using Amazon.DynamoDBv2;

namespace UFO.DLL.AWS.DynamoDB;

public class DynamoDBClient : Literal
{
    public readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly AmazonDynamoDBConfig _config;
    public readonly AmazonDynamoDBClient Client;

    public DynamoDBClient(string url, string accessKey, string secretKey)
        : base(TypeId.Z_CUSTOM)
    {
        _url = url;
        _accessKey = accessKey;
        _secretKey = secretKey;
        _config = new AmazonDynamoDBConfig
        {
            ServiceURL = url,
            UseHttp = true // Required for LocalStack (optional)
        };
        Client = new AmazonDynamoDBClient(accessKey, secretKey, _config);
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write($"DynamoDBClient{{\"{_url}\", \"{_accessKey}\"}}");
    }
}
