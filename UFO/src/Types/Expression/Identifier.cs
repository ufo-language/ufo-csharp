using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Identifier : Expression
{

    public class UnboundIdentifierException(Identifier ident) : Exception($"Unbound identifier: {ident}")
    {
        public Identifier Ident { get; } = ident;

        public override string ToString()
        {
            return $"{base.ToString()}, Identifier: {Ident}";
        }
    }

    private static readonly Dictionary<string, Identifier> _internedIdentifiers = [];
    private static readonly object _dictionaryLock = new();

    public string Name { get; private set; }
    private readonly int HashCode;

    private static readonly int HashSeed = typeof(Identifier).GetHashCode();

    private Identifier(string name)
    {
        Name = name;
        HashCode = Utils.Hash.CombineHash(HashSeed, Name.GetHashCode());
    }

    public static Identifier Create(string name)
    {
        lock (_dictionaryLock)
        {
            if (!_internedIdentifiers.TryGetValue(name, out Identifier? value))
            {
                value = new Identifier(name);
                _internedIdentifiers[name] = value;
            }
            return value;
        }
    }

    public override bool EqualsAux(UFOObject other)
    {
        Identifier otherIdentifier = (Identifier)other;
        return Name == otherIdentifier.Name;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject value = Nil.Create();
        if (etor.Lookup(this, ref value))
        {
            return value;
        }
        throw new UnboundIdentifierException(this);
    }

    public override int GetHashCode()
    {
        return HashCode;
    }

    public override bool Match(UFOObject other, ref Evaluator.Evaluator etor)
    {
        etor.Bind(this, other);
        return true;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write(Name);
    }

}
