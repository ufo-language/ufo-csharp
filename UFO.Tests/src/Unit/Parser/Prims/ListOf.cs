using UFO.Lexer;
using UFO.Parser;
using UFO.Parser.Prims;

namespace UFO.Tests.Unit.Parser.Prims;

public class ListOfTests
{

    private static ListOf ProperListOfParser = 
         new(open: new Spot(TokenType.Special, "["),
             elem: new Spot(TokenType.Integer),
             sep: new Spot(TokenType.Special, ","),
             close: new Spot(TokenType.Special, "]"));

    [Fact]
    public void ListOf_EmptyString_ReturnsFalse()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new([], tokens);

        // Act
        bool success = ProperListOfParser.Parse(parserState);

        // Assert
        Assert.False(success);
    }

    [Fact]
    public void ListOf_0_ReturnsEmptyList()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("[]");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new([], tokens);

        // Act
        bool success = ProperListOfParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Empty(objList);
    }

    [Fact]
    public void ListOf_1_ReturnsList()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("[100]");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new([], tokens);

        // Act
        bool success = ProperListOfParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Single(objList);
        object elem = objList[0];
        Assert.IsType<Token>(elem);
        Token token = (Token)elem;
        Assert.Equal("100", token.Lexeme);
    }

    [Fact]
    public void ListOf_2_ReturnsList()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("[100, 200]");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new([], tokens);

        // Act
        bool success = ProperListOfParser.Parse(parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<List>(value);
        List objList = (List)value;
        Assert.Equal(2, objList.Count);
        object elem = objList[0];
        Assert.IsType<Token>(elem);
        Token token = (Token)elem;
        Assert.Equal("100", token.Lexeme);
        elem = objList[1];
        Assert.IsType<Token>(elem);
        token = (Token)elem;
        Assert.Equal("200", token.Lexeme);
    }

}
