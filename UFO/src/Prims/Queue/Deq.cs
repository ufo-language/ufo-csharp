using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

[PrimName("queue", "deq")]
public class Deq : Primitive
{
    public Deq(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.QUEUE]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Queue q = (Types.Data.Queue)args[0];
        if (q.Deq(out UFOObject elem))
        {
            return elem;
        }
        throw new Exception("Queue empty");
    }
}
