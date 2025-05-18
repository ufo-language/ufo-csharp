using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Queue;

public class Deq : Primitive
{
    public Deq() : base("deq")
    {
        ParamTypes = [
            [TypeId.QUEUE]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Queue q = (Types.Data.Queue)args[0];
        UFOObject elem;
        if (q.Deq(out elem))
        {
            return elem;
        }
        throw new Exception("Queue empty");
    }
}
