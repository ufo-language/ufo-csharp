namespace UFO.Types.Literal;

public class Integer : Literal
{
    private static readonly int HashSeed = typeof(Integer).GetHashCode();
    public readonly int Value;

    private Integer(int value)
        : base(TypeId.INTEGER)
    {
        Value = value;
    }

    public static Integer Create(int value)
    {
        return new Integer(value);
    }

    public override bool BoolValue => Value != 0;

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
