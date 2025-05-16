using UFO.Lexer;
using UFO.Parser;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Parser.Grammar;

public class IfThenTests
{
    [Fact]
    public void IfThen_WithElse()
    {
        // Arrange
        string inputString = "if true then 100 else 200";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("IfThen", parserState);

        // Assert
        Assert.True(success);
        object valueObj = parserState.Value;
        Assert.IsType<IfThen>(valueObj);
        IfThen value = (IfThen)valueObj;
        Assert.Equal(inputString, value.ToString());
    }

    [Fact]
    public void IfThen_WithoutElse()
    {
        // Arrange
        string inputString = "if true then 100";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("IfThen", parserState);

        // Assert
        Assert.True(success);
        object valueObj = parserState.Value;
        Assert.IsType<IfThen>(valueObj);
        IfThen value = (IfThen)valueObj;
        Assert.Equal(inputString, value.ToString());
    }

}
