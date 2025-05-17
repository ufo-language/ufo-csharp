using Amazon.S3;
using Amazon.S3.Model;
using System.Text;

using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class Put : Primitive
{

    // private readonly string _DEFAULT_URL = "http://localhost:4566";

    public Put() : base("put")
    {
        ParamTypes = [
            [TypeId.HASH_TABLE,  // credentials
             TypeId.STRING,      // bucket name
             TypeId.STRING,      // key
             TypeId.STRING],     // data
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.HashTable credentials = (Types.Data.HashTable)args[0];
        string bucketName = args[1].ToDisplayString();
        string key = args[2].ToDisplayString();
        string data = args[3].ToDisplayString();
        // extract credentials strings
        // string url = credentials.Get(Credentials.URLSymbol, Types.Literal.String.Create(_DEFAULT_URL)).ToDisplayString();
        // string accessKey = credentials.Get(Credentials.AccessKeySymbol, Types.Literal.String.Create("")).ToDisplayString();
        // string secretKey = credentials.Get(Credentials.SecretKeySymbol, Types.Literal.String.Create("")).ToDisplayString();
        // upload the data
        // Task task = Upload(url, accessKey, secretKey, bucketName, key, data);
        // task.GetAwaiter().GetResult();
        return Nil.NIL;
    }

    static async Task Upload(string url, string accessKey, string secretKey, string bucketName, string key, string data)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = url,
            ForcePathStyle = true // Required for LocalStack
        };

        var client = new AmazonS3Client("fake-access-key", "fake-secret-key", config);

        // Make sure the bucket exists
        await client.PutBucketAsync(bucketName);

        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = new MemoryStream(Encoding.UTF8.GetBytes(data))
        };

        var response = await client.PutObjectAsync(request);
        Console.WriteLine($"Stored {key} in {bucketName}: {response.HttpStatusCode}");
    }

}
