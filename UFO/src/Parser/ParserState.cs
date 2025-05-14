using UFO.Lexer;
using UFO.Types;

namespace UFO.Parser;

public class ParserState(Dictionary<string, IParser> parserTable, List<Token> tokens)
{

    public Dictionary<string, IParser> ParserTable { get; private set; } = parserTable;
    public int TokenIndex { get; private set; } = 0;
    public string CurrentParserName = "";
    public object Value = new();
    public (string, int) MemoKey = ("", -1);
    private readonly Dictionary<(string, int), (bool, object)> MemoTable = [];
    public readonly List<Token> Tokens = tokens;

    public void Advance()
    {
        TokenIndex++;
    }

    public bool FindMemo((string, int) memoKey, out (bool, object) value)
    {
        string parserName = memoKey.Item1;
        int tokenIndex = memoKey.Item2;
        return MemoTable.TryGetValue((parserName, tokenIndex), out value);
    }

    public void Memoize(string name, int index, bool success, object value)
    {
        MemoTable[(name, index)] = (success, value);
    }

    public Token NextToken()
    {
        return Tokens[TokenIndex];
    }

    public (int, object) GetState()
    {
        return (TokenIndex, Value);
    }

    public void RestoreState((int, object) state)
    {
        TokenIndex = state.Item1;
        Value = state.Item2;
    }

}
