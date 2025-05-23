using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.List;

public class Map : Primitive
{
    public Map()
    {
        ParamTypes = [
            [TypeId.LIST, TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.List list = (Types.Data.List)args[0];
        UFOObject function = args[1];
        Types.Data.Queue resultQ = Types.Data.Queue.Create();
        foreach (UFOObject elem in list.EachElem())
        {
            UFOObject value = function.Apply(etor, [elem]);
            resultQ.Enq(value);
        }
        return resultQ.AsList();
    }
}
