using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.NS;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "ns";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim_manual(new Global(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
