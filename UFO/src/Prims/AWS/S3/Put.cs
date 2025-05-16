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

    public override UFOObject Call(Evaluator.Evaluator etor, List args)
    {
        throw new NotImplementedException();
    }
}
