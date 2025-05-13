using System.Diagnostics.CodeAnalysis;
using System.Text;

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

    private static readonly Dictionary<string, Identifier> _internedIdentifiers = new Dictionary<string, Identifier>();

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
        if (!_internedIdentifiers.TryGetValue(name, out Identifier? value))
        {
            value = new Identifier(name);
            _internedIdentifiers[name] = value;
        }
        return value;
    }

    public override bool EqualsAux(UFOObject other)
    {
        Identifier otherIdentifier = (Identifier)other;
        return Name == otherIdentifier.Name;
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        UFOObject value = default!;
        if (etor.Lookup_Rel(this, ref value)) etor.PushObj(value);
        else throw new UnboundIdentifierException(this);
    }

    public override int GetHashCode()
    {
        return HashCode;
    }

    public override void ToString(StringBuilder sb)
    {
        sb.Append(Name);
    }

}
