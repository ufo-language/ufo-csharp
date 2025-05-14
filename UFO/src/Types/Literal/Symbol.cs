using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Symbol : Literal
{
    private static readonly Dictionary<string, Symbol> _internedSymbols = [];
    private static readonly object _dictionaryLock = new();

    public string Name { get; private set; }
    private readonly int HashCode;

    private static readonly int HashSeed = typeof(Symbol).GetHashCode();

    private Symbol(string name)
    {
        Name = name;
        HashCode = Utils.Hash.CombineHash(HashSeed, Name.GetHashCode());
    }

    public static Symbol Create(string name)
    {
        lock (_dictionaryLock)
        {
            if (!_internedSymbols.TryGetValue(name, out Symbol? value))
            {
                value = new Symbol(name);
                _internedSymbols[name] = value;
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
        return HashCode;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write(Name);
    }

}
