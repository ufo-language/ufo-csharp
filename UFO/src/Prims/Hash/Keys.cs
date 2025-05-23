using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Hash;

public class Keys : Primitive
{
    public Keys()
    {
        ParamTypes = [
            [TypeId.HASH_TABLE],
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        HashTable hash = (HashTable)args[0];
        return hash.Keys();
    }
}
