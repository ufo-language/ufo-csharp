using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Io;


public class Show : Primitive
{
    public Show()
    {
        ParamTypes_SumOfProds([]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        foreach (UFOObject arg in args)
        {
            arg.ShowOn(Console.Out);
        }
        return Nil.Create();
    }
}
