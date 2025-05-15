namespace UFO.Parser.Prims;

public class Returning(object value, object parser) : IParser
{

    private readonly object _value = value;
    private readonly object _parser = parser;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            parserState.Value = _value;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Returning({_value}, {_parser})";
    }

}
