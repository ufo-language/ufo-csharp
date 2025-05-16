using UFO.Types;
using UFO.Types.Expression;

namespace UFO.Parser.Prims;

public class RecursionBarrier() : IParser
{

    public class DummyObject
    {
        public override string ToString() => "DUMMY_OBJECT";
    }

    private readonly DummyObject _DUMMY_OBJECT = new();

    public bool Parse(ParserState parserState)
    {
        string parserName = parserState.CurrentParserName;
        parserState.Memoize((parserName, parserState.TokenIndex), false, _DUMMY_OBJECT, -1);
        parserState.Value = Parser.IGNORE;
        return true;
    }

    public override string ToString()
    {
        return "RecursionBarrier()";
    }

}
