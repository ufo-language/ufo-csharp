using UFO.Lexer;

namespace UFO.Tests.Unit.Lexer;

public class LexerTests
{
    // private static List<Token> Lex(string input) => new UFO.Lexer.Lexer(input).Tokenize();
    private static List<Token> Lex(string input)
    {
        UFO.Lexer.Lexer lexer = new(input);
        List<Token> tokens = lexer.Tokenize();
        return tokens;
    }

    [Fact]
    public void LexesInteger()
    {
        var tokens = Lex("123");
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.Integer, tokens[0].Type);
        Assert.Equal("123", tokens[0].Lexeme);
    }

    [Fact]
    public void LexesReal()
    {
        var tokens = Lex("3.14");
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.Real, tokens[0].Type);
        Assert.Equal("3.14", tokens[0].Lexeme);
    }

    [Fact]
    public void LexesSignedNumbers()
    {
        var tokens = Lex("+42 -7.5");
        Assert.Equal(3, tokens.Count);
        Assert.Equal("+42", tokens[0].Lexeme);
        Assert.Equal("-7.5", tokens[1].Lexeme);
    }

    [Fact]
    public void LexesWords()
    {
        var tokens = Lex("foo Bar");
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Word, tokens[0].Type);
        Assert.Equal(TokenType.Symbol, tokens[1].Type);
    }

    [Fact]
    public void LexesStringWithEscapes()
    {
        var tokens = Lex("\"hello\\nworld\\\"!\"");
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.String, tokens[0].Type);
        Assert.Equal("hello\nworld\"!", tokens[0].Lexeme);
    }

    [Fact]
    public void ThrowsOnUnterminatedString()
    {
        var ex = Assert.Throws<Exception>(() => Lex("\"unterminated"));
        Assert.Contains("Unterminated string", ex.Message);
    }

    [Fact]
    public void LexesOperators()
    {
        var tokens = Lex("++ -- && || * / +");
        Assert.Equal(8, tokens.Count);
        foreach (var token in tokens.Take(tokens.Count - 1))
        {
            Assert.Equal(TokenType.Operator, token.Type);
        }
        Assert.Equal(TokenType.EOI, tokens.Last().Type);
        Assert.Equal("EOI", tokens.Last().Lexeme);
    }

    [Fact]
    public void LexesSpecialCharacters()
    {
        var tokens = Lex("{}[]()");
        Assert.Equal(7, tokens.Count);
        foreach (var token in tokens.Take(tokens.Count - 1))
        {
            Assert.Equal(TokenType.Special, token.Type);
        }
        Assert.Equal(TokenType.EOI, tokens.Last().Type);
        Assert.Equal("EOI", tokens.Last().Lexeme);
    }

    [Fact]
    public void SkipsLineComment()
    {
        var tokens = Lex("42 // comment\n 7");
        Assert.Equal(3, tokens.Count);
        Assert.Equal("42", tokens[0].Lexeme);
        Assert.Equal("7", tokens[1].Lexeme);
    }

    [Fact]
    public void SkipsBlockComment()
    {
        var tokens = Lex("foo /* ignore me */ bar");
        Assert.Equal(3, tokens.Count);
        Assert.Equal("foo", tokens[0].Lexeme);
        Assert.Equal("bar", tokens[1].Lexeme);
    }

    [Fact]
    public void TracksPosition()
    {
        var tokens = Lex("a\n  b\n   c");
        Assert.Equal(4, tokens.Count);
        Assert.Equal((0, 1, 0), tokens[0].Position); // 'a' at start
        Assert.Equal((2, 2, 4), tokens[1].Position); // 'b' after \n and 2 spaces
        Assert.Equal((3, 3, 9), tokens[2].Position); // 'c' after another \n and 3 spaces
    }

}
