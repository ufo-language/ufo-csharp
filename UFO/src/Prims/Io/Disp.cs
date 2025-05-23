using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Io;

public class Disp : Primitive
{
    public Disp()
    {
        ParamTypes_SumOfProds([]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        foreach (UFOObject arg in args)
        {
            arg.DisplayOn(Console.Out);
        }
        return Nil.Create();
    }
}
