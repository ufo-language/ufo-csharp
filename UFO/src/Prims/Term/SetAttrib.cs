using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Term;

public class SetAttrib : Primitive
{
    public SetAttrib()
    {
        ParamTypes = [
            [TypeId.TERM, TypeId.Z_ANY],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Term term = (Types.Data.Term)args[0];
        UFOObject attrib = args[1];
        term.Attrib = attrib;
        return term;
    }
}
