using UFO.Lexer;

namespace UFO.Parser.Prims;

public class Spot : IParser
{
    private TokenType _tokenType { get; }
    private string? LexemeString { get; }

    public Spot(TokenType tokenType)
    {
        _tokenType = tokenType;
        LexemeString = null;
    }

    public Spot(TokenType tokenType, string lexemeString)
    {
        _tokenType = tokenType;
        LexemeString = lexemeString;
    }

    public bool Parse(ParserState parserState)
    {
        Token token = parserState.NextToken;
        if (token.Type != _tokenType || (LexemeString != null && token.Lexeme != LexemeString))
            return false;
        parserState.Value = token;
        parserState.Advance();
        return true;
    }

    public override string ToString()
    {
        if (LexemeString != null)
        {
            return $"Spot({_tokenType}, '{LexemeString}')";
        }
        return $"Spot({_tokenType})";
    }

}
