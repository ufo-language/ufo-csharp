using Amazon.S3.Model;
using System.Net;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class Get : Primitive
{

    public Get() : base("get")
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
        GetObjectResponse response = DoGet(s3ClientObj, bucketName, key).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            using StreamReader reader = new(response.ResponseStream);
            string body = reader.ReadToEnd();
            return Term.Create(statusCodeSymbol, Types.Literal.String.Create(body));
        }
        return statusCodeSymbol;
    }

    static async Task<GetObjectResponse> DoGet(S3Client s3Client, string bucketName, string key)
    {
        GetObjectRequest request = new()
        {
            BucketName = bucketName,
            Key = key
        };
        return await s3Client.Client.GetObjectAsync(request);
    }
}
