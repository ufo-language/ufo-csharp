using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.AWS.S3;

[PrimName("aws", "s3", "client")]
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

#if false
    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        HashTable credentialsHash = HashTable.Create();
        UFOObject urlObj = args[0];
        if (!urlObj.BoolValue)
        {
            urlObj = Types.Literal.String.Create("http://localhost:4566");
        }
        credentialsHash[URLSymbol] = urlObj;
        credentialsHash[AccessKeySymbol] = args[1];
        credentialsHash[SecretKeySymbol] = args[2];
        return credentialsHash;
    }
#endif

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        UFOObject urlObj = args[0];
        string url = urlObj.BoolValue ? urlObj.ToDisplayString() : _DEFAULT_URL; ;
        string accessKey = args[1].ToDisplayString();
        string secretKey = args[2].ToDisplayString();
        return new S3Client(url, accessKey, secretKey);
    }

}