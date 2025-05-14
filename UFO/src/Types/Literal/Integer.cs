using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Integer : Literal
{

    private static readonly int HashSeed = typeof(Integer).GetHashCode();

    private readonly int _value;

    private Integer(int value)
    {
        _value = value;
    }

    public static Integer Create(int value)
    {
        return new Integer(value);
    }

    public override bool BoolValue()
    {
        return _value != 0;
    }

    public override bool EqualsAux(UFOObject otherObj)
    {
        Integer other = (Integer)otherObj;
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
