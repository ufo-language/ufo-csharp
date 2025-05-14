using UFO.Types.Expression;

namespace UFO.Parser.Prims;

public class Apply(Func<object, object> function, object parser) : IParser
{

    private readonly Func<object, object> _function = function;
    private readonly object _parser = parser;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            parserState.Value = _function(parserState.Value);
            return true;
        }
        return false;
    }
}
