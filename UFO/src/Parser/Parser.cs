using UFO.Lexer;

namespace UFO.Parser;

public class Parser
{

    public static readonly object IGNORE = new();

    public static bool Parse(string parserName, ParserState parserState)
    {
        parserState.CurrentParserName = parserName;
        (string, int) memoKey = (parserName, parserState.TokenIndex);
        if (parserState.FindMemo(memoKey, out (bool, object) memoValue))
        {
            parserState.Value = memoValue.Item2;
            return memoValue.Item1;
        }
        // Lookup the parser.
        parserState.MemoKey = memoKey;
        bool success = false;
        int savedIndex = parserState.TokenIndex;
        if (parserState.ParserTable.TryGetValue(parserName, out IParser? parser))
        {
            // Memoize the result.
            success = Parse(parser!, parserState);
            parserState.Memoize(parserName, savedIndex, success, parserState.Value);
            return success;
        }
        if (char.IsUpper(parserName[0]))
        {
            throw new Exception($"Unknown parser '{parserName}'");
        }
        // Look for a lexeme with that name.
        Token token = parserState.NextToken();
        if (token.Lexeme == parserName)
        {
            parserState.Value = IGNORE;
            parserState.Advance();
            success = true;
        }
        else
        {
            success = false;
        }
        // Memoize the result.
        parserState.Memoize(parserName, savedIndex, success, parserState.Value);
        return success;
    }

    public static bool Parse(IParser parser, ParserState parserState)
    {
        return parser.Parse(parserState);
    }

    public static bool Parse(object parserObj, ParserState parserState)
    {
        if (parserObj is string parserName)
        {
            return Parse(parserName, parserState);
        }
        if (parserObj is IParser parser)
        {
            return Parse(parser, parserState);
        }
        throw new Exception($"Unknown parser type: {parserObj}");
    }

}
