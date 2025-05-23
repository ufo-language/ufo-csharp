using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.List;

public class ExcludeIf : Primitive
{
    public ExcludeIf()
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
            if (!value.BoolValue)
            {
                resultQ.Enq(elem);
            }
        }
        return resultQ.AsList();
    }
}
