using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Real : Literal
{

    private static readonly int HashSeed = typeof(Real).GetHashCode();

    private readonly double _value;

    private Real(double value)
    {
        _value = value;
    }

    public static Real Create(double value)
    {
        return new Real(value);
    }

    public override bool BoolValue()
    {
        return _value != 0.0;
    }

    public override bool EqualsAux(UFOObject otherObj)
    {
        Real other = (Real)otherObj;
        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(HashSeed, _value.GetHashCode());
    }
    
    public override void ShowOn(TextWriter writer)
    {
        writer.Write(_value);
    }

}
