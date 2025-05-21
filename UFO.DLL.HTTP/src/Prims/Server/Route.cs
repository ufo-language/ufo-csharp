using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.HTTP.Server;

[PrimName("http", "server", "route")]
public class Route : Primitive
{
    public Route(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.Z_CUSTOM /*HTTPServer*/, TypeId.STRING /*path*/, TypeId.Z_ANY /*handler*/]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Console.Error.WriteLine($"Primitive {Name} is not implememted");
        return Nil.NIL;
    }
}
