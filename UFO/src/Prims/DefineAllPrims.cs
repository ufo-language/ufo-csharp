using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefineAllPrims
{
    public static void DefPrim(Primitive prim, HashTable ns, string ns_name)
    {
        ns[Identifier.Create(prim.Name)] = prim;
    }

    public static void DefPrims(Evaluator.Evaluator etor)
    {
        // The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
        IO.A_DefinePrims.DefinePrims(etor);
        Array.A_DefinePrims.DefinePrims(etor);
    }
}
