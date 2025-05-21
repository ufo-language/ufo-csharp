using Amazon.S3.Model;
using System.Text;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

[PrimName("aws", "s3", "put")]
public class Put : Primitive
{
    public Put(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client
             TypeId.STRING,    // bucket name
             TypeId.STRING,    // key
             TypeId.STRING     // data
            ]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        if (args[0] is not S3Client s3ClientObj)
        {
            throw new UFOException("S3Client", [
                ("Message", Types.Literal.String.Create("Expected an S3Client instance")),
                ("Actual", args[0]),
                ("Type", args[0].TypeSymbol())
            ]);
        }
        string bucketName = args[1].ToDisplayString();
        string key = args[2].ToDisplayString();
        string data = args[3].ToDisplayString();
        PutObjectResponse response = DoPut(s3ClientObj, bucketName, key, data).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        return statusCodeSymbol;
    }

    static async Task<PutObjectResponse> DoPut(S3Client s3Client, string bucketName, string key, string data)
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
