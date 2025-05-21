using UFO.Evaluator;
using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.HTTP.Server;

[PrimName("http", "server", "new")]
public class New : Primitive
{
    public New(string longName) : base(longName)
    {
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        return new UFO.DLL.HTTP.HttpServer();
    }
}
