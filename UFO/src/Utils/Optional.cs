using UFO.Types.Literal;

namespace UFO.Utils;

public class Optional
{
    public static readonly Optional EMPTY = new Optional();
    public readonly bool HasValue = false;
    public readonly UFO.Types.UFOObject Value = Nil.NIL;
    public Optional()
    { }
    public Optional(UFO.Types.UFOObject value)
    {
        HasValue = true;
        Value = value;
    }
}
