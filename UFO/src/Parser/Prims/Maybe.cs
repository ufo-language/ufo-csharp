using UFO.Types.Expression;

namespace UFO.Parser.Prims;

public class Maybe(object parser) : IParser
{

    private readonly object _parser = parser;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            return true;
        }
        parserState.Value = Parser.IGNORE;
        return true;
    }
}
