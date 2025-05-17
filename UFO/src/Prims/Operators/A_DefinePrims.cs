using UFO.Prims.Operator;

namespace UFO.Prims.Operators;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(Evaluator.Evaluator etor)
    {
        // Operators are defined at the top level.
        DefineAllPrims.DefPrim(new Load(), etor);
    }
}
