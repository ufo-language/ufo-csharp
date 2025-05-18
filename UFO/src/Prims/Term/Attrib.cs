using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Term;

public class Attrib : Primitive
{
    public Attrib() : base("attrib")
    {
        ParamTypes = [
            [TypeId.TERM],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Term term = (Types.Data.Term)args[0];
        return term.Attrib;
    }
}
