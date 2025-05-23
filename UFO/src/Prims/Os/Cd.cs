using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Os;

public class Cd : Primitive
{
    public Cd()
    {
        ParamTypes = [
            [TypeId.STRING]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        string dirName = ((Types.Literal.String)args[0]).Value;
        try
        {
            Directory.SetCurrentDirectory(dirName);
        }
        catch (Exception exn)
        {
            return Types.Data.Term.Create(Symbol.Create("Fail"), Types.Literal.String.Create(exn.Message));
        }
        return Nil.NIL;
    }
}
