
using UFO.Lexer;

namespace UFO.Tests;

public class SpotTests
{

    [Fact]
    public void Spot_Symbol()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        Parser.Prims.Spot spot = new(TokenType.Symbol);
        Parser.ParserState parserState = new(tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        Token? token = parserState.Token;
        Assert.NotNull(token);
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

    [Fact]
    public void Spot_Integer()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123 Abc");
        List<Token> tokens = lexer.Tokenize();
        Parser.Prims.Spot spot = new(TokenType.Integer);
        Parser.ParserState parserState = new(tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        Token? token = parserState.Token;
        Assert.NotNull(token);
        Assert.Equal(TokenType.Integer, token.Type);
        Assert.Equal("123", token.Lexeme);
    }

    [Fact]
    public void SpotSpecific_Word()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("abc 123");
        List<Token> tokens = lexer.Tokenize();
        Parser.Prims.Spot spot = new(TokenType.Word, "abc");
        Parser.ParserState parserState = new(tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        Token? token = parserState.Token;
        Assert.NotNull(token);
        Assert.Equal(TokenType.Word, token.Type);
        Assert.Equal("abc", token.Lexeme);
    }

}
