using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.IO;

public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        string nsName = "io";
        HashTable ns = HashTable.Create();
        DefineAllPrims.DefPrim(new Display(), ns, nsName);
        DefineAllPrims.DefPrim(new Show(), ns, nsName);
        etor.Bind(Identifier.Create(nsName), ns);
    }
}
