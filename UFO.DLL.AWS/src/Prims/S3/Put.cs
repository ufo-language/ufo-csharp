using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
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
            [TypeId.Z_CUSTOM,  // client
             TypeId.STRING,    // bucket name
             TypeId.STRING,    // key
             TypeId.STRING],   // data
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        UFOObject clientObj = args[0];
        if (clientObj is not S3Client)
        {
            throw new Exception($"Expected S3Client, found {clientObj} :: {clientObj.TypeSymbol()}");
        }
        S3Client s3ClientObj = (S3Client)clientObj;
        string bucketName = args[1].ToDisplayString();
        string key = args[2].ToDisplayString();
        string data = args[3].ToDisplayString();
        PutObjectResponse response = Upload(s3ClientObj, bucketName, key, data).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        return statusCodeSymbol;
    }

    static async Task Upload_X(S3Client s3Client, string bucketName, string key, string data)
    {
        // Make sure the bucket exists
        await s3Client.Client.PutBucketAsync(bucketName);

        PutObjectRequest request = new()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = new MemoryStream(Encoding.UTF8.GetBytes(data))
        };
        PutObjectResponse response = await s3Client.Client.PutObjectAsync(request);
        Console.WriteLine($"Stored {key} in {bucketName}: {response.HttpStatusCode}");
    }

    static async Task<PutObjectResponse> Upload(S3Client s3Client, string bucketName, string key, string data)
    {
        // Create the bucket if it does not exist.
        await s3Client.Client.PutBucketAsync(bucketName);
        // Create the request and then send it.
        PutObjectRequest request = new()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = new MemoryStream(Encoding.UTF8.GetBytes(data))
        };
        return await s3Client.Client.PutObjectAsync(request);
    }

}
