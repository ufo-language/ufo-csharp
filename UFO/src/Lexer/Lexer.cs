using System.Text;

namespace UFO.Lexer;

public static class Constants
{
    public static readonly string Operators = "+-*/&|=<>!";
    public static readonly List<string> ReservedWords = [
        "false", "fun", "for", "in", "nil", "true"
    ];
}

public enum TokenType {
    Integer, Real,
    Word, ReservedWord, Symbol,
    String,
    Operator,
    Special,
    Comment,
    EOI
}

public record Token(TokenType Type, string Lexeme, (int Col, int Line, int Index) Position) {
    public override string ToString() {
        var (col, line, idx) = Position;
        return $"Token{{{Type}, \"{Lexeme}\", line={line}, col={col}, index={idx}}}";
    }
}

public class Lexer {
    private string _inputString = "";
    private int _index = 0;
    private int _line = 1;
    private int _col = 0;

    public Lexer() => _inputString = "";
    public Lexer(string input) => this._inputString = input;

    public static void PrintTokens(List<Token> tokens)
    {
        Console.WriteLine("Tokens:");
        foreach (Token token in tokens)
        {
            Console.Write(token);
            Console.Write(", ");
        }
        Console.WriteLine();
    }
    
    public List<Token> Tokenize(string inputString)
    {
        _inputString = inputString;
        return Tokenize();
    }

    public List<Token> Tokenize() {
        var tokens = new List<Token>();

        while (_index < _inputString.Length) {
            SkipWhitespace();

            if (_index >= _inputString.Length)
                break;

            var startIdx = _index;
            var startCol = _col;
            var startLine = _line;

            char c = Peek();

            // Handle comments
            if (c == '/') {
                if (Match("//")) {
                    while (_index < _inputString.Length && Peek() != '\n') Advance();
                    continue;
                } else if (Match("/*")) {
                    while (_index < _inputString.Length && !(Peek() == '*' && Peek(1) == '/')) {
                        if (Peek() == '\n') AdvanceLine();
                        else Advance();
                    }
                    if (Match("*/")) Advance(2);
                    continue;
                }
            }

            // String literal
            if (c == '"') {
                Advance(); // consume opening "
                var sb = new StringBuilder();
                bool terminated = false;

                while (_index < _inputString.Length) {
                    if (Peek() == '"') {
                        Advance(); // closing quote
                        terminated = true;
                        break;
                    }

                    if (Peek() == '\\') {
                        Advance();
                        char esc = Peek();
                        switch (esc) {
                            case 'n': sb.Append('\n'); break;
                            case 't': sb.Append('\t'); break;
                            case 'r': sb.Append('\r'); break;
                            case '\\': sb.Append('\\'); break;
                            case '"': sb.Append('"'); break;
                            default: sb.Append(esc); break; // unrecognized escape
                        }
                        Advance();
                    } else {
                        if (Peek() == '\n')
                            throw new Exception($"Unterminated string at line {_line}, col {_col}");
                        sb.Append(Peek());
                        Advance();
                    }
                }

                if (!terminated)
                    throw new Exception($"Unterminated string at end of input (started at line {startLine}, col {startCol})");

                tokens.Add(new Token(TokenType.String, sb.ToString(), (startCol, startLine, startIdx)));
                continue;
            }

            // Number (possibly with + or -)
            if (char.IsDigit(c) || ((c == '+' || c == '-') && char.IsDigit(Peek(1)))) {
                var sb = new StringBuilder();
                sb.Append(Peek());
                Advance();
                while (_index < _inputString.Length && char.IsDigit(Peek())) {
                    sb.Append(Peek());
                    Advance();
                }
                if (Peek() == '.' && char.IsDigit(Peek(1))) {
                    sb.Append('.');
                    Advance();
                    while (_index < _inputString.Length && char.IsDigit(Peek())) {
                        sb.Append(Peek());
                        Advance();
                    }
                    tokens.Add(new Token(TokenType.Real, sb.ToString(), (startCol, startLine, startIdx)));
                } else {
                    tokens.Add(new Token(TokenType.Integer, sb.ToString(), (startCol, startLine, startIdx)));
                }
                continue;
            }

            // Word (lower or upper case start)
            if (char.IsLetter(c)) {
                var sb = new StringBuilder();
                sb.Append(Peek());
                Advance();
                while (_index < _inputString.Length && char.IsLetterOrDigit(Peek())) {
                    sb.Append(Peek());
                    Advance();
                }
                TokenType type;
                string word = sb.ToString();
                if (char.IsUpper(sb[0]))
                {
                    type = TokenType.Symbol;
                }
                else
                {
                    type = Constants.ReservedWords.Contains(word) ? TokenType.ReservedWord : TokenType.Word;
                }
                tokens.Add(new Token(type, sb.ToString(), (startCol, startLine, startIdx)));
                continue;
            }

            // Operators |
            if (Constants.Operators.Contains(c)) {
                var sb = new StringBuilder();
                while (_index < _inputString.Length && Constants.Operators.Contains(Peek())) {
                    sb.Append(Peek());
                    Advance();
                }
                tokens.Add(new Token(TokenType.Operator, sb.ToString(), (startCol, startLine, startIdx)));
                continue;
            }

            // If it's none of the above, then it's a special character.
            tokens.Add(new Token(TokenType.Special, c.ToString(), (startCol, startLine, startIdx)));
            Advance();
        }
        tokens.Add(new Token(TokenType.EOI, "EOI", (_col, _line, _index)));
        return tokens;
    }

    private void SkipWhitespace() {
        while (_index < _inputString.Length && char.IsWhiteSpace(Peek())) {
            if (Peek() == '\n') AdvanceLine();
            else Advance();
        }
    }

    private char Peek(int ahead = 0) => (_index + ahead < _inputString.Length) ? _inputString[_index + ahead] : '\0';

    private void Advance(int amount = 1) {
        for (int i = 0; i < amount; i++) {
            if (_index < _inputString.Length) {
                if (_inputString[_index] == '\n') {
                    AdvanceLine();
                } else {
                    _col++;
                }
                _index++;
            }
        }
    }

    private void AdvanceLine() {
        _line++;
        _col = 0;
        _index++;
    }

    private bool Match(string s) {
        if (_index + s.Length > _inputString.Length) return false;
        for (int i = 0; i < s.Length; i++) {
            if (_inputString[_index + i] != s[i]) return false;
        }
        return true;
    }
}
