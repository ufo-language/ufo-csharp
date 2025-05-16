namespace UFO.Parser.Prims;

public class Ignore : IParser
{

    private readonly object _parser;

    public Ignore(object parser)
    {
        _parser = parser;
    }

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            parserState.Value = Parser.IGNORE;
            return true;
        }
        return false;
    }

}
