using UFO.Evaluator;
using UFO.Prims.IO;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace Prims;

public class DefinePrims
{

    private static void DefinePrim(Primitive prim, Evaluator etor)
    {
        string name = prim.Name;
        etor.Bind(Identifier.Create(name), prim);
    }

    public static void DefineAllPrims(Evaluator etor)
    {
        DefinePrim(new Display(), etor);
        DefinePrim(new Show(), etor);
    }
}
