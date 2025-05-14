
using UFO.Lexer;

namespace UFO.Tests.Unit.Parser.Prims;

public class OneOfTests
{

    [Fact]
    public void OneOf_Parse1of2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spotSymbol = new(TokenType.Symbol);
        UFO.Parser.Prims.Spot spotInteger = new(TokenType.Integer);
        UFO.Parser.Prims.OneOf oneOfParser = new(spotSymbol, spotInteger);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = oneOfParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

    [Fact]
    public void OneOf_Parse2of2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spotSymbol = new(TokenType.Symbol);
        UFO.Parser.Prims.Spot spotInteger = new(TokenType.Integer);
        UFO.Parser.Prims.OneOf oneOfParser = new(spotSymbol, spotInteger);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = oneOfParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Integer, token.Type);
        Assert.Equal("123", token.Lexeme);
    }

}
