using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.Array;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "array";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim_manual(new Length(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
