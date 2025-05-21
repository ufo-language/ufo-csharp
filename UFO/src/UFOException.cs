using System.Text;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO;

public class UFOException : Exception
{
    public readonly Symbol Name;
    public readonly Types.Data.Array Payload;

    public UFOException(string name, params (string key, UFOObject value)[] kwargs)
        : base(name)
    {
        Name = Symbol.Create(name);
        Types.Data.Array elems = Types.Data.Array.Create();
        foreach ((string key, UFOObject value) in kwargs)
        {
            UFOObject keyObj;
            if (char.IsUpper(key[0]))
            {
                keyObj = Symbol.Create(key);
            }
            else if (char.IsLower(key[0]))
            {
                keyObj = Identifier.Create(key);
            }
            else
            {
                keyObj = Types.Literal.String.Create(key);
            }
            elems.Append(Binding.Create(keyObj, value));
        }
        Payload = elems;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Message).Append('\n');
        foreach (UFOObject elem in Payload.EachElem())
        {
            sb.Append("  ").Append(elem.ToString()).Append('\n');
        }
        return sb.ToString();
    }
}
