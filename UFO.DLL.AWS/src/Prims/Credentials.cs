using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.AWS.S3;

public class Credentials : Primitive
{
    public static readonly Symbol URLSymbol = Symbol.Create("URL");
    public static readonly Symbol AccessKeySymbol = Symbol.Create("AccessKey");
    public static readonly Symbol SecretKeySymbol = Symbol.Create("SecretKey");

    public Credentials() : base("credentials")
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
}