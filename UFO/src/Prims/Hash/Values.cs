using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Array;

[PrimName("hash", "values")]
public class Values : Primitive
{
    public Values(string longName) : base(longName)
    {
        ParamTypes = [
            [TypeId.HASH_TABLE],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        HashTable hash = (HashTable)args[0];
        return hash.Values();
    }
}
