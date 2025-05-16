using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.Array;

public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "array";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim(new Length(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
