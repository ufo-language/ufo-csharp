using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Hash;

public class Map : Primitive
{
    public Map()
    {
        ParamTypes = [
            [TypeId.HASH_TABLE, TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        HashTable hash = (HashTable)args[0];
        UFOObject function = args[1];
        HashTable resultHash = HashTable.Create();
        foreach (KeyValuePair<UFOObject, UFOObject> pair in hash.EachElem())
        {
            UFOObject value = function.Apply(etor, [pair.Key, pair.Value]);
            resultHash.Add(value);
        }
        return resultHash;
    }
}
