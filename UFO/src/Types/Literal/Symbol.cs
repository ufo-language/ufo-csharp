namespace UFO.Types.Literal;

public class Symbol : Literal
{
    private static readonly Dictionary<string, Symbol> _INTERNED_SYMBOLS = [];
    private static readonly Lock _DICTIONARY_LOCK = new();
    public string Name { get; private set; }
    private readonly int _hashCode;
    private static readonly int _HASH_SEED = typeof(Symbol).GetHashCode();

    private Symbol(string name)
        : base(TypeId.SYMBOL)
    {
        Name = name;
        _hashCode = Utils.Hash.CombineHash(_HASH_SEED, Name.GetHashCode());
    }

    public static Symbol Create(string name)
    {
        lock (_DICTIONARY_LOCK)
        {
            if (!_INTERNED_SYMBOLS.TryGetValue(name, out Symbol? value))
            {
                value = new Symbol(name);
                _INTERNED_SYMBOLS[name] = value;
            }
            return value;
        }
    }

    public override bool EqualsAux(UFOObject other)
    {
        Symbol otherSymbol = (Symbol)other;
        return Name == otherSymbol.Name;
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write(Name);
    }
}
