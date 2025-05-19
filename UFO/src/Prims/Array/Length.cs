using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Array;

[PrimAttrib("array", "length")]
public class Length : Primitive
{
    public Length() : base("length")
    {
        ParamTypes = [
            [TypeId.ARRAY],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Array array = (Types.Data.Array)args[0];
        return Integer.Create(array.Count);
    }
}
