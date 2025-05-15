using System.Text;

namespace UFO.Parser.Prims;

public class OneOf(params object[] parsers) : IParser
{

    private object[] _parsers = parsers;

    public bool Parse(ParserState parserState)
    {
        (int, object) longestResult = default;
        int longestParse = -1;
        (int, object) savedState = parserState.GetState();
        for (int n=0; n<_parsers.Length; n++)
        {
            object parserObj = _parsers[n];
            if (Parser.Parse(parserObj, parserState))
            {
                (int, object) currentState = parserState.GetState();
                if (currentState.Item1 > longestParse)
                {
                    longestResult = currentState;
                    longestParse = currentState.Item1;
                }
            }
            parserState.RestoreState(savedState);
        }
        if (longestParse == -1)
        {

            return false;
        }
        parserState.RestoreState(longestResult);
        return true;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("OneOf(");
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
