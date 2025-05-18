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

    private static readonly Dictionary<string, Identifier> _INTERNED_IDENTIFIERS = [];
    private static readonly Lock _DICTIONARY_LOCK = new();
    public string Name { get; private set; }
    private readonly int _hashCode;
    private static readonly int _HASH_SEED = typeof(Identifier).GetHashCode();

    private Identifier(string name)
        : base(TypeId.IDENTIFIER)
    {
        Name = name;
        _hashCode = Utils.Hash.CombineHash(_HASH_SEED, Name.GetHashCode());
    }

    public static Identifier Create(string name)
    {
        lock (_DICTIONARY_LOCK)
        {
            if (!_INTERNED_IDENTIFIERS.TryGetValue(name, out Identifier? value))
            {
                value = new Identifier(name);
                _INTERNED_IDENTIFIERS[name] = value;
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
        return _hashCode;
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
