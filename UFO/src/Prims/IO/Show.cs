using UFO.Evaluator;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.IO;

[PrimName("io", "show")]

public class Show(string longName) : Primitive(longName)
{
    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        foreach (UFOObject arg in args)
        {
            arg.ShowOn(Console.Out);
        }
        return Nil.Create();
    }
}
