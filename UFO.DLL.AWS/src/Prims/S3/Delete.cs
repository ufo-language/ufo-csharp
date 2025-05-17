using Amazon.S3.Model;
using System.Net;

using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class Delete : Primitive
{
    public Delete() : base("delete")
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
        UFOObject clientObj = args[0];
        if (clientObj is not S3Client)
        {
            throw new Exception($"Expected S3Client, found {clientObj} :: {clientObj.TypeSymbol()}");
        }
        S3Client s3ClientObj = (S3Client)clientObj;
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
