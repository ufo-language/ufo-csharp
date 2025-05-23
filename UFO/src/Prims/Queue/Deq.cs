using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

public class Deq : Primitive
{
    public Deq()
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
