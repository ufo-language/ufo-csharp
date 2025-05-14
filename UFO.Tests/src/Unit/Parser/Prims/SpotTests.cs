using UFO.Lexer;

namespace UFO.Tests.Unit.Parser.Prims;

public class SpotTests
{

    [Fact]
    public void Spot_Symbol()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

    [Fact]
    public void Spot_Integer()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123 Abc");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Integer);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Integer, token.Type);
        Assert.Equal("123", token.Lexeme);
    }

    [Fact]
    public void SpotSpecific_Word()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Word, "abc");
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Word, token.Type);
        Assert.Equal("abc", token.Lexeme);
    }

    [Fact]
    public void Spot_ReservedWord()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("fun 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.ReservedWord, "fun");
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.ReservedWord, token.Type);
        Assert.Equal("fun", token.Lexeme);
    }

    [Fact]
    public void Spot_Special()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("()");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Special, "(");
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = spot.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Special, token.Type);
        Assert.Equal("(", token.Lexeme);
    }

}
