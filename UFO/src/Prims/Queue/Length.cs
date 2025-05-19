using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

[PrimAttrib("queue", "length")]
public class Length : Primitive
{
    public Length() : base("length")
    {
        ParamTypes = [
            [TypeId.QUEUE]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Queue q = (Types.Data.Queue)args[0];
        return Integer.Create(q.Count);
    }
}
