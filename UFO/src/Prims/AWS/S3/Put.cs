using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.AWS.S3;

public class Put : Primitive
{

    public Put() : base("put")
    {
        ParamTypes = [
            [TypeId.STRING, TypeId.STRING, TypeId.STRING],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        throw new NotImplementedException();
    }
}
