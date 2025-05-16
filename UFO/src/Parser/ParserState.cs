using UFO.Lexer;
using UFO.Types;

namespace UFO.Parser;

public class ParserState(Dictionary<string, IParser> parserTable, List<Token> tokens)
{

    public Dictionary<string, IParser> ParserTable { get; private set; } = parserTable;
    public int TokenIndex = 0;
    public string CurrentParserName = "";
    public object Value = new();
    public (string, int) MemoKey = ("", -1);
    private readonly Dictionary<(string, int), (bool, object, int)> MemoTable = [];
    public readonly List<Token> Tokens = tokens;

    public void Advance()
    {
        TokenIndex++;
    }

    public bool FindMemo((string, int) memoKey, out (bool, object, int) value)
    {
        return MemoTable.TryGetValue(memoKey, out value);
    }

    public void Memoize((string, int) memoKey, bool success, object value, int index)
    {
        MemoTable[memoKey] = (success, value, index);
    }

    public Token NextToken => Tokens[TokenIndex];

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
