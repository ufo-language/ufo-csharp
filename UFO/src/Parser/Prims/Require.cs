namespace UFO.Parser.Prims;

public class Require(object parser, string? message = null) : IParser
{

    private readonly object _parser = parser;
    private readonly string? _message = message;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            return true;
        }
        throw new Exception(_message ?? $"Parse failure: expected {_parser}");
    }

    public override string ToString()
    {
        return $"Require({_parser})";
    }

}
