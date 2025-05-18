using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.OS;

public class CWD : Primitive
{
    public CWD() : base("cwd")
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
