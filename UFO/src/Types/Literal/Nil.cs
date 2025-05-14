using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Text;

namespace UFO.Types.Literal;

public class Nil : Literal
{
    public static readonly Nil NIL = new();

    private static readonly int HashCode = typeof(Nil).GetHashCode();

    private Nil()
    {}

    public static Nil Create()
    {
        return NIL;
    }

    public override bool BoolValue => false;

    public override bool EqualsAux(UFOObject other)
    {
        return true;
    }

    public override int GetHashCode()
    {
        return Nil.HashCode;
    }
    
    public override void ShowOn(TextWriter writer)
    {
        writer.Write("nil");
    }

}
