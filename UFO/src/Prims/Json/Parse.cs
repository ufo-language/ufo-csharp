using System.Text.Json;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims.Json;

public class Parse : Primitive
{
    public Parse()
    {
        ParamTypes = [
            [TypeId.STRING]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Literal.String jsonString = (Types.Literal.String)args[0];
        using JsonDocument doc = JsonDocument.Parse(jsonString.Value);
        JsonElement root = doc.RootElement;
        return WalkTree(root);
    }

    private UFOObject WalkTree(JsonElement elem)
    {
        switch (elem.ValueKind)
        {
            case JsonValueKind.Object:
                HashTable hash = HashTable.Create();
                foreach (var prop in elem.EnumerateObject())
                {
                    HashTable propHash = HashTable.Create();
                    UFOObject key = char.IsLower(prop.Name[0]) ? Identifier.Create(prop.Name) : Symbol.Create(prop.Name);
                    hash[key] = WalkTree(prop.Value);
                }
                return hash;
            case JsonValueKind.Array:
                Types.Data.Array array = Types.Data.Array.Create();
                foreach (var item in elem.EnumerateArray())
                {
                    array.Append(WalkTree(item));
                }
                return array;
            case JsonValueKind.String:
                return Types.Literal.String.Create(elem.GetString() ?? "");
            case JsonValueKind.Number:
                return Integer.Create(elem.GetInt32());
            case JsonValueKind.True:
                return Types.Literal.Boolean.TRUE;
            case JsonValueKind.False:
                return Types.Literal.Boolean.FALSE;
            case JsonValueKind.Null:
                return Nil.NIL;
            default:
                throw new Exception($"{Name} got unknown element '{elem}'");
        }
    }

}
