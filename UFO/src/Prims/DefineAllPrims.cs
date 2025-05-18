using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefineAllPrims
{
    // Helper method to define a primitive in a namespace.
    public static void DefPrim(Primitive prim, HashTable ns, string ns_name)
    {
        ns[Identifier.Create(prim.Name)] = prim;
    }

    public static void DefPrim(Primitive prim, Evaluator.Evaluator etor)
    {
        etor.Bind(Identifier.Create(prim.Name), prim);
    }

    public static void DefPrims(Evaluator.Evaluator etor)
    {
        // The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
        Array.A_DefinePrims.DefinePrims(etor);
        IO.A_DefinePrims.DefinePrims(etor);
        Operators.A_DefinePrims.DefinePrims(etor);
        OS.A_DefinePrims.DefinePrims(etor);
        Queue.A_DefinePrims.DefinePrims(etor);
        Term.A_DefinePrims.DefinePrims(etor);
    }
}
