using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Literal;

public class Integer : Literal
{

    private static readonly int HashSeed = typeof(Integer).GetHashCode();

    private readonly int Value;

    private Integer(int value)
    {
        Value = value;
    }

    public static Integer Create(int value)
    {
        return new Integer(value);
    }

    public override bool BoolValue()
    {
        return Value != 0;
    }

    public override bool EqualsAux(UFOObject otherObj)
    {
        Integer other = (Integer)otherObj;
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
