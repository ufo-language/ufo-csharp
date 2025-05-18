using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.OS;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "os";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim(new CD(), ns, nsName);
        DefineAllPrims.DefPrim(new CWD(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
