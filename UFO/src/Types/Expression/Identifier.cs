using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Literal;

public class Identifier : Literal
{

    public class UnboundIdentifierException(Identifier ident) : Exception($"Unbound identifier: {ident}")
    {
        public Identifier Ident { get; } = ident;

        public override string ToString()
        {
            return $"{base.ToString()}, Identifier: {Ident}";
        }
    }

    private static readonly Dictionary<string, Identifier> _internedIdentifiers = new Dictionary<string, Identifier>();

    public string Name { get; private set; }
    private readonly int HashCode;

    private static readonly int HashSeed = typeof(Identifier).GetHashCode();

    private Identifier([NotNull] string name)
    {
        Name = name;
        HashCode = Utils.Hash.CombineHash(HashSeed, Name.GetHashCode());
    }

    public static Identifier Create([NotNull] string name)
    {
        if (!_internedIdentifiers.ContainsKey(name))
        {
            _internedIdentifiers[name] = new Identifier(name);
        }
        return _internedIdentifiers[name];
    }

    public override bool EqualsAux([NotNull] UFOObject other)
    {
        Identifier otherIdentifier = (Identifier)other;
        return Name == otherIdentifier.Name;
    }

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        UFOObject value = default!;
        if (etor.Lookup_Rel(this, ref value))
        {
            etor.PushObj(value);
        }
        else
        {
            throw new UnboundIdentifierException(this);
        }
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
