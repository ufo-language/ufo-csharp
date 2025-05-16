using UFO.Lexer;
using UFO.Parser.Prims;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Parser;

public class Parser
{
    public class Ignore
    {
        public override string ToString() => "IGNORE";
    }

    public static readonly Ignore IGNORE = new();

    public static bool Parse(string parserName, ParserState parserState)
    {
        parserState.CurrentParserName = parserName;
        (string, int) memoKey = (parserName, parserState.TokenIndex);
        if (parserState.FindMemo(memoKey, out (bool, object, int) memoValue))
        {
            parserState.Value = memoValue.Item2;
            parserState.TokenIndex = memoValue.Item3;
            return memoValue.Item1;
        }
        // Lookup the parser.
        parserState.MemoKey = memoKey;
        bool success = false;
        if (parserState.ParserTable.TryGetValue(parserName, out IParser? parser))
        {
            // Memoize the result.
            success = Parse(parser!, parserState);
            parserState.Memoize(memoKey, success, parserState.Value, parserState.TokenIndex);
            return success;
        }
        if (char.IsUpper(parserName[0]))
        {
            throw new Exception($"Unknown parser '{parserName}'");
        }
        // Look for a lexeme with that name.
        Token token = parserState.NextToken;
        if (token.Lexeme == parserName)
        {
            // Found the token, but since we know exactly what that lexeme is
            // it can safely be ignored.
            parserState.Value = IGNORE;
            parserState.Advance();
            success = true;
        }
        else
        {
            success = false;
        }
        // Memoize the result.
        parserState.Memoize(memoKey, success, parserState.Value, parserState.TokenIndex);
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
