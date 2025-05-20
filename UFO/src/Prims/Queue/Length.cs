using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

[PrimName("queue", "length")]
public class Length : Primitive
{
    public Length(string longName) : base(longName)
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
