using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

[PrimName("queue", "enq")]
public class Enq : Primitive
{
    public Enq(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.QUEUE, TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Queue q = (Types.Data.Queue)args[0];
        UFOObject elem = args[1];
        q.Enq(elem);
        return q;
    }
}
