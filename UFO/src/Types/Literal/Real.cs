using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Real : Literal
{

    private static readonly int HashSeed = typeof(Real).GetHashCode();

    public readonly double Value;

    private Real(double value)
        : base(TypeId.REAL)
    {
        Value = value;
    }

    public static Real Create(double value)
    {
        return new Real(value);
    }

    public override bool BoolValue => Value != 0.0;

    public override bool EqualsAux(UFOObject otherObj)
    {
        Real other = (Real)otherObj;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(HashSeed, Value.GetHashCode());
    }
    
    public override void ShowOn(TextWriter writer)
    {
        writer.Write(Value);
    }

}
