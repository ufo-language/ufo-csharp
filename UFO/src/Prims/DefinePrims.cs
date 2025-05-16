using UFO.Prims.IO;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefinePrims
{

    private static void DefinePrim(Primitive prim, Evaluator.Evaluator etor)
    {
        etor.Bind(Identifier.Create(prim.Name), prim);
    }

    public static void DefineAllPrims(Evaluator.Evaluator etor)
    {
        DefinePrim(new Display(), etor);
        DefinePrim(new Show(), etor);
    }
}
