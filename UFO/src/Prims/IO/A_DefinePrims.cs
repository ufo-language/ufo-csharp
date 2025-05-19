using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.IO;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "io";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim_manual(new Display(), ns, nsName);
        DefineAllPrims.DefPrim_manual(new Show(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
