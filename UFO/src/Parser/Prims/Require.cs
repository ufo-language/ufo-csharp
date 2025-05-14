namespace UFO.Parser.Prims;

public class Require(object parser) : IParser
{

    private readonly object _parser = parser;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            return true;
        }
        throw new Exception($"Parse failure: expected {_parser}");
    }
}
