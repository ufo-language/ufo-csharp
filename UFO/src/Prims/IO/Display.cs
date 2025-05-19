using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

[PrimAttrib("io", "disp")]
public class Display : Primitive
{
    public Display() : base("disp")
    { }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        foreach (UFOObject arg in args)
        {
            arg.DisplayOn(Console.Out);
        }
        return Nil.Create();
    }
}
