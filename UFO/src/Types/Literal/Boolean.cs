using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Literal;

public class Boolean : Literal
{

    public static readonly Boolean TRUE = new();
    public static readonly Boolean FALSE = new();

    private static readonly int HashSeed = typeof(Integer).GetHashCode();

    private Boolean()
    {}

    public override bool BoolValue()
    {
        return ReferenceEquals(this, TRUE);
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(HashSeed, ((object)this).GetHashCode());
    }
    
    public override string ToString()
    {
        return BoolValue() ? "true" : "false";
    }

}
