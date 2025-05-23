using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Os;

public class Cwd : Primitive
{
    public Cwd()
    {
        ParamTypes = [
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        string cwd = System.IO.Directory.GetCurrentDirectory();
        return Types.Literal.String.Create(cwd);
    }
}
