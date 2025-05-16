using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

public class Show : Primitive
{
    public Show() : base("show")
    {}

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        foreach (UFOObject arg in args)
        {
            arg.DisplayOn(Console.Out);
        }
        return Nil.Create();
    }
}
