namespace UFO.Parser.Prims;

public class Succeed(object value) : IParser
{

    private readonly object _value = value;

    public bool Parse(ParserState parserState)
    {
        parserState.Value = _value;
        return true;
    }

    public override string ToString()
    {
        return $"Succeed({_value})";
    }
}
