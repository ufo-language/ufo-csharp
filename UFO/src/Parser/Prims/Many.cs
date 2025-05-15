using UFO.Types.Literal;

namespace UFO.Parser.Prims;

public class Many : IParser
{
    private readonly object _parser;
    private readonly int _min;

    public Many(object parser)
    {
        _parser = parser;
        _min = 0;
    }

    public Many(object parser, int min)
    {
        _parser = parser;
        _min = min;
    }

    public bool Parse(ParserState parserState)
    {
        List<object> elems = [];
        int n = 0;
        (int, object) savedState = parserState.GetState();
        while (Parser.Parse(_parser, parserState))
        {
            elems.Add(parserState.Value);
            n++;
        }
        if (n < _min)
        {
            parserState.RestoreState(savedState);
            return false;
        }
        parserState.Value = elems;
        return true;
    }
}
