using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Json;

public class Stringify : Primitive
{
    public Stringify()
    {
        ParamTypes = [
            [TypeId.Z_ANY]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        StringWriter sw = new();
        StringifyObject(args[0], sw);
        return Types.Literal.String.Create(sw.ToString());
    }

    private static void StringifyObject(UFOObject obj, StringWriter sw)
    {
        switch (obj.TypeId)
        {
            case TypeId.ARRAY:
                Types.Data.Array array = (Types.Data.Array)obj;
                Utils.ShowOn.ShowOnWith(sw, array.EachElem(), "[", ", ", "]");
                break;
            case TypeId.HASH_TABLE:
                HashTable hash = (HashTable)obj;
                sw.Write('{');
                bool firstIter = true;
                foreach (KeyValuePair<UFOObject, UFOObject> pair in hash.EachElem())
                {
                    if (firstIter) firstIter = false;
                    else sw.Write(',');
                    sw.Write($"\"{pair.Key}\":");
                    StringifyObject(pair.Value, sw);
                }
                sw.Write('}');
                break;
            case TypeId.NIL:
                sw.Write("null");
                break;
            default:
                obj.ShowOn(sw);
                break;
        }
    }
}
