using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

public class Display : Primitive
{
    public Display() : base("disp")
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
