namespace UFO.Parser.Prims;

public class IfThen(object parser, object value) : IParser
{

    private readonly object _parser = parser;
    private readonly object _value = value;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            parserState.Value = _value;
            return true;
        }
        return false;
    }
}
