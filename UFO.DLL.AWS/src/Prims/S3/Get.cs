using Amazon.S3.Model;
using System.Net;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class Get : Primitive
{

    public Get()
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
