using UFO.Types;
using UFO.Types.Literal;
using Amazon.S3;

namespace UFO.DLL.AWS.S3;

public class S3Client : Literal
{
    public readonly string _url;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly AmazonS3Config _config;
    public readonly AmazonS3Client Client;

    public S3Client(string url, string accessKey, string secretKey)
        : base(TypeId.Z_CUSTOM)
    {
        _url = url;
        _accessKey = accessKey;
        _secretKey = secretKey;
        _config = new AmazonS3Config
        {
            ServiceURL = url,
            ForcePathStyle = true // Required for LocalStack
        };
        Client = new AmazonS3Client(accessKey, secretKey, _config);
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write($"S3Client{{\"{_url}\", \"{_accessKey}\"}}");
    }
}
