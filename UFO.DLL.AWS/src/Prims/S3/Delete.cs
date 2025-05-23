using Amazon.S3.Model;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class Delete : Primitive
{
    public Delete()
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client
             TypeId.STRING,    // bucket name
             TypeId.STRING     // key
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
        DeleteObjectResponse response = DoDelete(s3ClientObj, bucketName, key).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        return statusCodeSymbol;
    }

    static async Task<DeleteObjectResponse> DoDelete(S3Client s3Client, string bucketName, string key)
    {
        DeleteObjectRequest request = new()
        {
            BucketName = bucketName,
            Key = key
        };
        return await s3Client.Client.DeleteObjectAsync(request);
    }
}
