using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Literal;

public class Nil : Literal
{
    private static readonly Nil INSTANCE = new();

    private static readonly int HashCode = typeof(Nil).GetHashCode();

    private Nil()
    {}

    public static Nil Create()
    {
        return INSTANCE;
    }

    public override bool BoolValue()
    {
        return false;
    }

    public override bool EqualsAux([NotNull] UFOObject other)
    {
        return true;
    }

    public override int GetHashCode()
    {
        return Nil.HashCode;
    }
    
    public override string ToString()
    {
        return "nil";
    }

}
