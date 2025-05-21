using Amazon.S3.Model;
using System.Net;

using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

[PrimName("aws", "s3", "list")]
public class List : Primitive
{
    public List(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM,  // client
             TypeId.STRING     // bucket name
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
