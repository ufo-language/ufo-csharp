using System.Reflection;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Ns;

public class Global : Primitive
{

    public Global()
    {
        ParamTypes_SumOfProds([TypeId.SYMBOL]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Binding env = etor.Env;
        Types.Data.Queue keyQ = Types.Data.Queue.Create();
        foreach (Binding binding in env.EachElem())
        {
            keyQ.Enq(binding.Key);
        }
        return keyQ.AsList();
    }
}
