using UFO.Lexer;
using UFO.Parser;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Parser;

public class UFOGrammarTests
{
    [Fact]
    public void Grammar_TestIfItWorksAtAll()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Integer", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Integer>(value);
        Integer intValue = (Integer)value;
        Assert.Equal(123, intValue.Value);
    }

    [Fact]
    public void Grammar_Literal_Integer()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Integer>(value);
        Integer intValue = (Integer)value;
        Assert.Equal(123, intValue.Value);
    }

    [Fact]
    public void Grammar_Literal_Symbol()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Symbol>(value);
        Symbol symbolValue = (Symbol)value;
        Assert.Equal("Abc", symbolValue.Name);
    }

    [Fact]
    public void Grammar_Literal_Real()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123.5");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Real>(value);
        Real realValue = (Real)value;
        Assert.Equal(123.5, realValue.Value);
    }

    [Fact]
    public void Grammar_Literal_Boolean_true()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("true");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Literal.Boolean>(value);
        UFO.Types.Literal.Boolean boolValue = (UFO.Types.Literal.Boolean)value;
        Assert.True(boolValue.BoolValue);
    }

    [Fact]
    public void Grammar_Literal_Boolean_false()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("false");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Literal.Boolean>(value);
        UFO.Types.Literal.Boolean boolValue = (UFO.Types.Literal.Boolean)value;
        Assert.False(boolValue.BoolValue);
    }

    [Fact]
    public void Grammar_Literal_Nil()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("nil");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Literal.Nil>(value);
    }

}
