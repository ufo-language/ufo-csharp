using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.List;

public class ForEach : Primitive
{
    public ForEach()
    {
        ParamTypes = [
            [TypeId.LIST, TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.List list = (Types.Data.List)args[0];
        UFOObject function = args[1];
        foreach (UFOObject elem in list.EachElem())
        {
            function.Apply(etor, [elem]);
        }
        return list;
    }
}
