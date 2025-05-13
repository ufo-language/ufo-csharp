using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class HashTable : Data
{
    private readonly Dictionary<UFOObject, UFOObject> Dict;

    public int Count { get { return Dict.Count; } }

    /// <summary>
    /// Creates a new HashTable.
    /// </summary>
    /// <param name="elems">The bindings of the HashTable as a linear array in key / value order.</param>
    /// <returns>The new HashTable.</returns>
    private HashTable(params UFOObject[] elems)
    {
        Dict = [];
        bool keyIter = true;
        UFOObject key = Nil.Create();
        UFOObject value;
        foreach (UFOObject elem in elems)
        {
            if (keyIter)
            {
                key = elem;
                keyIter = false;
            }
            else
            {
                value = elem;
                Dict[key] = value;
                keyIter = true;
            }
        }
    }

    public static HashTable Create(params UFOObject[] elems)
    {
        return new(elems);
    }

    public UFOObject this[UFOObject index]
    {
        get => Dict[index];
        set => Dict[index] = value;
    }

    public IEnumerable<KeyValuePair<UFOObject, UFOObject>> EachElem()
    {
        foreach (KeyValuePair<UFOObject, UFOObject> pair in Dict)
        {
            yield return pair;
        }
        yield break;
    }

    public IEnumerable<Binding> EachElemAsBinding()
    {
        foreach (KeyValuePair<UFOObject, UFOObject> pair in Dict)
        {
            yield return Binding.Create(pair.Key, pair.Value);
        }
        yield break;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        HashTable newHashTable = new();
        foreach (KeyValuePair<UFOObject, UFOObject> pair in EachElem())
        {
            UFOObject keyValue = pair.Key.Eval(etor);
            UFOObject valueValue = pair.Value.Eval(etor);
            newHashTable[keyValue] = valueValue;
        }
        return newHashTable;
    }

    public bool Get(UFOObject key, out UFOObject elem)
    {
        bool found = Dict.TryGetValue(key, out UFOObject? elem1);
        elem = found ? elem1! : Nil.Create();
        return found;
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElemAsBinding(), "#{", ", ", "}");
    }

}
