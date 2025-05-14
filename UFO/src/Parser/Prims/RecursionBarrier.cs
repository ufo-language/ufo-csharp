using UFO.Types.Expression;

namespace UFO.Parser.Prims;

public class RecursionBarrier() : IParser
{

    private readonly object _DUMMY_OBJECT = new();

    public bool Parse(ParserState parserState)
    {
        string parserName = parserState.CurrentParserName;
        parserState.Memoize(parserName, parserState.TokenIndex, false, _DUMMY_OBJECT);
        parserState.Value = Parser.IGNORE;
        return true;
    }
}
