using Amazon.S3.Model;
using System.Net;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

public class List : Primitive
{
    public List() : base("list")
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client
             TypeId.STRING     // bucket name
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
        ListObjectsV2Response response = DoList(s3ClientObj, bucketName).GetAwaiter().GetResult();
        Symbol statusCodeSymbol = Symbol.Create(response.HttpStatusCode.ToString());
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            Queue q = Queue.Create();
            foreach (S3Object obj in response.S3Objects)
            {
                q.Enq(Types.Literal.String.Create(obj.Key));
            }
            return Term.Create(statusCodeSymbol, q);
        }
        return statusCodeSymbol;
    }

    static async Task<ListObjectsV2Response> DoList(S3Client s3Client, string bucketName)
    {
        ListObjectsV2Request request = new()
        {
            BucketName = bucketName
        };
        return await s3Client.Client.ListObjectsV2Async(request);
    }
}
