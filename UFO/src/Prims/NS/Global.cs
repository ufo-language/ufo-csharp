using System.Reflection;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.NS;

[PrimName("ns", "global")]
public class Global : Primitive
{

    public Global(string longName) : base(longName)
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
