using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.AWS.Prims;

public class DefinePrims
{
    public static UFOObject DefPrims(Evaluator.Evaluator etor)
    {
        string nsName = "aws";
        HashTable ns = HashTable.Create();
        Lambda.A_DefinePrims.DefinePrims(ns, nsName);
        S3.A_DefinePrims.DefinePrims(ns, nsName);
        SQS.A_DefinePrims.DefinePrims(ns, nsName);
        Identifier awsId = Identifier.Create(nsName);
        etor.Bind(awsId, ns);
        return awsId;
    }
}
