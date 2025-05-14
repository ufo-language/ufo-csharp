
using UFO.Lexer;

namespace UFO.Tests.Unit.Parser.Prims;

public class SeqTests
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
        object? value = parserState.Value;
        Assert.NotNull(value);
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

}
