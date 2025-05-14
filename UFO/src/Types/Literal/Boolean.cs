using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Boolean : Literal
{

    public static readonly Boolean TRUE = new();
    public static readonly Boolean FALSE = new();

    private static readonly int _HASH_SEED = typeof(Integer).GetHashCode();

    private Boolean()
    {}

    public override bool BoolValue()
    {
        return ReferenceEquals(this, TRUE);
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(_HASH_SEED, ((object)this).GetHashCode());
    }
    
    public override void ShowOn(TextWriter writer)
    {
        writer.Write(BoolValue() ? "true" : "false");
    }

}
