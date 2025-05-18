namespace UFO.Types.Literal;

public class Boolean : Literal
{
    public static readonly Boolean TRUE = new();
    public static readonly Boolean FALSE = new();

    private static readonly int _HASH_SEED = typeof(Integer).GetHashCode();

    private Boolean()
        : base(TypeId.BOOLEAN)
    {}

    public static Boolean Create(bool b)
    {
        return b ? TRUE : FALSE;
    }

    public override bool BoolValue => ReferenceEquals(this, TRUE);

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(_HASH_SEED, ((object)this).GetHashCode());
    }
    
    public override void ShowOn(TextWriter writer)
    {
        writer.Write(BoolValue ? "true" : "false");
    }
}
