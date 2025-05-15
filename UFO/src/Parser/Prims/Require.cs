namespace UFO.Parser.Prims;

public class Require : IParser
{

    private readonly object _parser;
    private readonly string? _message;

    public Require(object parser)
    {
        _parser = parser;
        _message = null;
    }

    public Require(object parser, string message)
    {
        _parser = parser;
        _message = message;
    }

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
