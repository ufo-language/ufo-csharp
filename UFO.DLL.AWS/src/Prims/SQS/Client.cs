using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.SQS;

[PrimName("aws", "sqs", "client")]
public class Client : Primitive
{
    public static readonly Symbol URLSymbol = Symbol.Create("URL");
    public static readonly Symbol AccessKeySymbol = Symbol.Create("AccessKey");
    public static readonly Symbol SecretKeySymbol = Symbol.Create("SecretKey");
    private readonly string _DEFAULT_URL = "http://localhost:4566";


    public Client(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_ANY,  // AWS URL; String, or "" or nil or false for default
             TypeId.STRING, // access key
             TypeId.STRING  // secret key
            ],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        UFOObject urlObj = args[0];
        string url = urlObj.BoolValue ? urlObj.ToDisplayString() : _DEFAULT_URL; ;
        string accessKey = args[1].ToDisplayString();
        string secretKey = args[2].ToDisplayString();
        return new SQSClient(url, accessKey, secretKey);
    }

}