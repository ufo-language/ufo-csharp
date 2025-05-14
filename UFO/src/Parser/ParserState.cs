using UFO.Lexer;
using UFO.Types;

namespace UFO.Parser;

public class ParserState(List<Token> tokens)
{

    private readonly List<Token> Tokens = tokens;
    private int TokenIndex = 0;
    public Token? Token = null;
    public UFOObject? Value = null;

    public Token NextToken()
    {
        return Tokens[TokenIndex];
    }

    public void Advance()
    {
        TokenIndex++;
    }

}
