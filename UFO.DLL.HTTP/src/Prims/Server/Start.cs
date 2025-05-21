using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.HTTP.Server;

[PrimName("http", "server", "start")]
public class Start : Primitive
{
    public Start(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM /*HTTPServer*/]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Console.Error.WriteLine($"Primitive {Name} is not implememted");
        return Nil.NIL;
    }
}
