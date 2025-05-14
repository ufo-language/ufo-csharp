using UFO.Lexer;
using UFO.Parser;

namespace UFO.Parser.Prims;

public class Spot : Parser
{
    private TokenType TokenType { get; }
    private string? LexemeString { get; }

    public Spot(TokenType tokenType)
    {
        TokenType = tokenType;
        LexemeString = null;
    }

    public Spot(TokenType tokenType, string lexemeString)
    {
        TokenType = tokenType;
        LexemeString = lexemeString;
    }

    public override bool Parse(ParserState parserState)
    {
        Token token = parserState.NextToken();
        if (token.Type != TokenType || (LexemeString != null && token.Lexeme != LexemeString))
            return false;
        parserState.Token = token;
        parserState.Advance();
        return true;
    }

}
