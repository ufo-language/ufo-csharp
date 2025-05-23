using UFO.Evaluator;
using UFO.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.DLL.HTTP.Server;

public class New : Primitive
{
    public New()
    {
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        return new UFO.DLL.HTTP.HttpServer();
    }
}
