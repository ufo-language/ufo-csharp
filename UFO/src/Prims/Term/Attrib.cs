using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Term;

[PrimName("term", "attrib")]
public class Attrib : Primitive
{
    public Attrib(string longName) : base(longName)
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
