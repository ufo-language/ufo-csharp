using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

public class Show : Primitive
{
    public Show() : base("io_show")
    {}

    public override UFOObject Call(Evaluator.Evaluator etor, Pair args)
    {
        foreach (UFOObject arg in args.EachElem())
        {
            Console.Write(arg.ToString());
        }
        return Nil.Create();
    }
}
