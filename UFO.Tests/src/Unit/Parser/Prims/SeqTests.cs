using UFO.Lexer;
using UFO.Parser;

namespace UFO.Tests.Unit.Parser.Prims;

public class SeqTests
{

    [Fact]
    public void Seq_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spotSymbol = new(TokenType.Symbol);
        UFO.Parser.Prims.Spot spotInteger = new(TokenType.Integer);
        UFO.Parser.Prims.Seq seqParser = new(spotSymbol, spotInteger);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = seqParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List list = (List)value;
        Assert.Equal(2, list.Count);
        Token token0 = (Token)list[0];
        Assert.Equal(TokenType.Symbol, token0.Type);
        Assert.Equal("Abc", token0.Lexeme);
        Token token1 = (Token)list[1];
        Assert.Equal(TokenType.Integer, token1.Type);
        Assert.Equal("123", token1.Lexeme);
    }

}
