using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Array;

[PrimName("array", "forEach")]
public class ForEach : Primitive
{
    public ForEach(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.ARRAY, TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Array array = (Types.Data.Array)args[0];
        UFOObject function = args[1];
        Types.Data.Array resultAry = Types.Data.Array.Create();
        foreach (UFOObject elem in array.EachElem())
        {
            UFOObject value = function.Apply(etor, [elem]);
            resultAry.Append(value);
        }
        return array;
    }
}
