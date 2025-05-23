using UFO.Types.Literal;

namespace UFO.Types.Data;

public class HashTable : Data
{
    public class ProtoHash : Expression.Expression
    {
        private readonly List<UFOObject> _elems = [];

        public ProtoHash()
            : base(TypeId.PROTO_HASH)
        { }

        public static ProtoHash Create(Parser.List elems)
        {
            ProtoHash protoHash = new();
            foreach (object elem in elems)
            {
                protoHash._elems.Add((UFOObject)elem);
            }
            return protoHash;
        }

        public override UFOObject Eval(Evaluator.Evaluator etor)
        {
            HashTable hashTable = new();
            foreach (UFOObject elem in _elems)
            {
                UFOObject value = elem.Eval(etor);
                if (value is not Binding)
                {
                    throw new Exception($"Binding expected, found {value}");
                }
                Binding binding = (Binding)value;
                hashTable[binding.Key] = binding.Value;
            }
            return hashTable;
        }

        public override void ShowOn(TextWriter writer)
        {
            Utils.ShowOn.ShowOnWith(writer, _elems, "#{", ", ", "}");
        }

    }

    private readonly Dictionary<UFOObject, UFOObject> _dict;

    /// <summary>
    /// Creates a new HashTable. This is a convenience constructor.
    /// </summary>
    /// <param name="elems">The bindings of the HashTable are a linear array in key / value order.</param>
    /// <returns>The new HashTable.</returns>
    private HashTable(params UFOObject[] elems)
        : base(TypeId.HASH_TABLE)
    {
        _dict = [];
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
                _dict[key] = value;
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
        get => _dict[index];
        set => _dict[index] = value;
    }

    public void Add(object value)
    {
        if (value is Binding binding)
        {
            this[binding.Key] = binding.Value;
        }
        else if (value is List list)
        {
            this[list.First] = list.Rest;
        }
        else if (value is Array array && ((Array)value).Count == 2)
        {
            this[array[0]] = array[1];
        }
        else if (value is KeyValuePair<UFOObject, UFOObject> pair)
        {
            this[pair.Key] = pair.Value;
        }
        else
        {
            throw new Exception($"Illegal key/value pair: {value}");
        }
    }

    public override bool BoolValue => _dict.Count > 0;

    public int Count { get { return _dict.Count; } }

    public IEnumerable<KeyValuePair<UFOObject, UFOObject>> EachElem()
    {
        foreach (KeyValuePair<UFOObject, UFOObject> pair in _dict)
        {
            yield return pair;
        }
        yield break;
    }

    public IEnumerable<Binding> EachElemAsBinding()
    {
        foreach (KeyValuePair<UFOObject, UFOObject> pair in _dict)
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

    public override bool Get(UFOObject key, out UFOObject elem)
    {
        bool found = _dict.TryGetValue(key, out UFOObject? elem1);
        elem = found ? elem1! : Nil.Create();
        return found;
    }

    public UFOObject Get(UFOObject key, UFOObject defaultValue)
    {
        if (Get(key, out UFOObject value))
        {
            return value;
        }
        return defaultValue;
    }

    public Set Keys()
    {
        Set keySet = Set.Create();
        Dictionary<UFOObject, UFOObject>.KeyCollection keys = _dict.Keys;
        foreach (UFOObject key in keys)
        {
            keySet.Add(key);
        }
        return keySet;
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElemAsBinding(), "#{", ", ", "}");
    }

    public Queue Values()
    {
        Queue valueQueue = Queue.Create();
        Dictionary<UFOObject, UFOObject>.ValueCollection values = _dict.Values;
        foreach (UFOObject value in values)
        {
            valueQueue.Enq(value);
        }
        return valueQueue;
    }
}
