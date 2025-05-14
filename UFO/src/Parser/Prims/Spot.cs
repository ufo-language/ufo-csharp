using UFO.Lexer;

namespace UFO.Parser.Prims;

public class Spot : IParser
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

    public bool Parse(ParserState parserState)
    {
        Token token = parserState.NextToken();
        if (token.Type != TokenType || (LexemeString != null && token.Lexeme != LexemeString))
            return false;
        parserState.Value = token;
        parserState.Advance();
        return true;
    }

}
