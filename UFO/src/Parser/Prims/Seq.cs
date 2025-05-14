namespace UFO.Parser.Prims;

public class Seq(params object[] parsers) : IParser
{

    private readonly object[] Parsers = parsers;

    public bool Parse(ParserState parserState)
    {
        (int, object) savedState = parserState.GetState();
        List<object> values = [];
        foreach (object parser in Parsers)
        {
            if (!Parser.Parse(parser, parserState))
            {
                parserState.RestoreState(savedState);
                return false;
            }
            object value = parserState.Value;
            if (!ReferenceEquals(value, Parser.IGNORE))
            {
                values.Add(value);
            }
        }
        if (values.Count == 1)
        {
            parserState.Value = values[0];
        }
        else
        {
            parserState.Value = values;
        }
        return true;
    }
}
