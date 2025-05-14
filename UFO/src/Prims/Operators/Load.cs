// Loads an external DLL

using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Operator;

public class Load : Primitive
{
    public Load() : base("load")
    {
        ParamTypes_SumOfProds([TypeId.SYMBOL]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List args)
    {
        throw new NotImplementedException();
    }

}
