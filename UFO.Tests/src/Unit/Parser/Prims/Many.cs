using UFO.Lexer;
using UFO.Parser;
using UFO.Parser.Prims;

namespace UFO.Tests.Unit.Parser.Prims;

public class ManyTests
{

    [Fact]
    public void Many_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Many many = new(intParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = many.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Empty(objList);
    }

    [Fact]
    public void Many_3()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("0 1 2");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Many many = new(intParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = many.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Equal(3, objList.Count);
    }

    [Fact]
    public void Many_TooFew_Fails()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("0 1 2");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Many many = new(intParser, 5);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = many.Parse(parserState);

        // Assert
        Assert.False(success);
    }

}
