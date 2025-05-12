using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Literal;

public class Symbol : Literal
{
    private static readonly Dictionary<string, Symbol> _internedSymbols = new Dictionary<string, Symbol>();

    public string Name { get; private set; }
    private readonly int HashCode;

    private static readonly int HashSeed = typeof(Symbol).GetHashCode();

    private Symbol([NotNull] string name)
    {
        Name = name;
        HashCode = Utils.Hash.CombineHash(HashSeed, Name.GetHashCode());
    }

    public static Symbol Create([NotNull] string name)
    {
        if (!_internedSymbols.ContainsKey(name))
        {
            _internedSymbols[name] = new Symbol(name);
        }
        return _internedSymbols[name];
    }

    public override bool EqualsAux([NotNull] UFOObject other)
    {
        Symbol otherSymbol = (Symbol)other;
        return Name == otherSymbol.Name;
    }

    public override int GetHashCode()
    {
        return HashCode;
    }

    public override string ToString()
    {
        return Name;
    }

}
