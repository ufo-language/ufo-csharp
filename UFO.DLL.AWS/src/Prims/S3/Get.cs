using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.AWS.S3;

public class Get : Primitive
{

    public Get() : base("get")
    {
        ParamTypes = [
            // [TypeId.STRING, TypeId.STRING, TypeId.STRING],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        return Nil.NIL;
    }
}