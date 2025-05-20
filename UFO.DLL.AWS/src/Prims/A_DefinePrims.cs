using UFO.Prims;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.AWS;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "aws";
        HashTable ns = HashTable.Create();
        S3.A_DefinePrims.DefinePrims(ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
