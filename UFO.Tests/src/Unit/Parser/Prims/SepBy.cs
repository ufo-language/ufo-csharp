using UFO.Lexer;
using UFO.Parser;
using UFO.Parser.Prims;

namespace UFO.Tests.Unit.Parser.Prims;

public class SepByTests
{

    [Fact]
    public void SepBy_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Spot commaParser = new(TokenType.Special, ",");
        SepBy sepBy = new(intParser, commaParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = sepBy.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value); 
        List objList = (List)value;
        Assert.Empty(objList);
    }

    [Fact]
    public void SepBy_1()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("1");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Spot commaParser = new(TokenType.Special, ",");
        SepBy sepBy = new(intParser, commaParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = sepBy.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Single(objList);
    }

    [Fact]
    public void SepBy_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("1,2");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Spot commaParser = new(TokenType.Special, ",");
        SepBy sepBy = new(intParser, commaParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        bool success = sepBy.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Equal(2, objList.Count);
    }

    [Fact]
    public void SepBy_MissingElem()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("1,");
        List<Token> tokens = lexer.Tokenize();
        Spot intParser = new(TokenType.Integer);
        Spot commaParser = new(TokenType.Special, ",");
        SepBy sepBy = new(intParser, commaParser);
        UFO.Parser.ParserState parserState = new([], tokens);

        // Act
        // bool success = sepBy.Parse(parserState);

        // Assert
        Assert.Throws<Exception>(() => sepBy.Parse(parserState));
    }
}
