using System.Text;

namespace UFO.Parser.Prims;

public class Seq(params object[] parsers) : IParser
{
    private readonly object[] _parsers = parsers;

    public bool Parse(ParserState parserState)
    {
        (int, object) savedState = parserState.GetState();
        List values = [];
        foreach (object parser in _parsers)
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

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Seq(");
        bool firstIter = true;
        foreach (object parser in _parsers)
        {
            if (firstIter) firstIter = false;
            else sb.Append(", ");
            sb.Append(parser);
        }
        sb.Append(')');
        return sb.ToString();
    }

}
