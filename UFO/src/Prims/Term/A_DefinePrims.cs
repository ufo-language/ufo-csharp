using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.Term;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "term";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim(new Attrib(), ns, nsName);
        DefineAllPrims.DefPrim(new SetAttrib(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
