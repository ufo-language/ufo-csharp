using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

public class Show : Primitive
{
    public Show() : base("show")
    {}

    public override UFOObject Call(Evaluator.Evaluator etor, List args)
    {
        foreach (UFOObject arg in args.EachElem())
        {
            arg.DisplayOn(Console.Out);
        }
        return Nil.Create();
    }
}
