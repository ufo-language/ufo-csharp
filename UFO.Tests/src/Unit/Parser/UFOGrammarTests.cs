using UFO.Lexer;
using UFO.Parser;
using UFO.Types.Expression;
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
    public void Grammar_Integer()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
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
    public void Grammar_Symbol()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc");
        List<Token> tokens = lexer.Tokenize();
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
    public void Grammar_Real()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123.5");
        List<Token> tokens = lexer.Tokenize();
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
    public void Grammar_Boolean_true()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("true");
        List<Token> tokens = lexer.Tokenize();
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
    public void Grammar_Boolean_false()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("false");
        List<Token> tokens = lexer.Tokenize();
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
    public void Grammar_Nil()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("nil");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Nil>(value);
    }

    [Fact]
    public void Grammar_Identifierl()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("abc");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Identifier>(value);
    }

    [Fact]
    public void Grammar_Seq_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("()");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Seq_1()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("(123)");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Seq_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("(123; Abc)");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Array_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(0, array.Count);
    }

    [Fact]
    public void Grammar_Array_1()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{123}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(1, array.Count);
    }

    [Fact]
    public void Grammar_Array_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{123, Abc}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(2, array.Count);
    }

}
