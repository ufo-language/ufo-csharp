
using UFO.Lexer;
using UFO.Parser;

namespace UFO.Tests.Unit.Parser;

public class ParserTests
{

    [Fact]
    public void Parse_SpotParserInstance()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        ParserState parserState = new([], tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse(spot, parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.NotNull(value);
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

    [Fact]
    public void Parse_NamedParser()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        Dictionary<string, IParser> parserTable = new() {
            {"SpotSymbol", spot}
        };
        ParserState parserState = new(parserTable, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("SpotSymbol", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.NotNull(value);
        Assert.IsType<Token>(value);
        Token token = (Token)value;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }

#if false
    // This depends on ParserState.MemoTable being public. I ran this test and it worked,
    // so then I made MemoTable private and disabled this test.
    [Fact]
    public void Parse_CheckMemoTable()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc 123");
        List<Token> tokens = lexer.Tokenize();
        UFO.Parser.Prims.Spot spot = new(TokenType.Symbol);
        Dictionary<string, IParser> parserTable = new() {
            {"SpotSymbol", spot}
        };
        ParserState parserState = new(parserTable, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("SpotSymbol", parserState);

        // Assert
        parserState.MemoTable.TryGetValue(("SpotSymbol", 0), out (bool, object) value);
        Assert.True(value.Item1);
        Assert.IsType<Token>(value.Item2);
        Token token = (Token)value.Item2;
        Assert.Equal(TokenType.Symbol, token.Type);
        Assert.Equal("Abc", token.Lexeme);
    }
#endif

}