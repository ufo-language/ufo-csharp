using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.OS;

[PrimName("os", "cwd")]
public class CWD : Primitive
{
    public CWD(string longName) : base(longName)
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
