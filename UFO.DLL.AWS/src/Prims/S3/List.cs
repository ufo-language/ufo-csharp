using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.AWS.S3;

public class List : Primitive
{

    public List() : base("list")
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