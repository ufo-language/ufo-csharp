using System.Text;

namespace UFO.Lexer;

public static class Constants
{
    public static readonly string Operators = "+-*/&|=<>!";
    public static readonly List<string> ReservedWords = [
        "fun", "for", "in"
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
    private readonly string input;
    private int index = 0;
    private int line = 1;
    private int col = 0;

    public Lexer(string input) => this.input = input;

    public List<Token> Tokenize() {
        var tokens = new List<Token>();

        while (index < input.Length) {
            SkipWhitespace();

            if (index >= input.Length)
                break;

            var startIdx = index;
            var startCol = col;
            var startLine = line;

            char c = Peek();

            // Handle comments
            if (c == '/') {
                if (Match("//")) {
                    while (index < input.Length && Peek() != '\n') Advance();
                    continue;
                } else if (Match("/*")) {
                    while (index < input.Length && !(Peek() == '*' && Peek(1) == '/')) {
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

                while (index < input.Length) {
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
                            throw new Exception($"Unterminated string at line {line}, col {col}");
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
                while (index < input.Length && char.IsDigit(Peek())) {
                    sb.Append(Peek());
                    Advance();
                }
                if (Peek() == '.' && char.IsDigit(Peek(1))) {
                    sb.Append('.');
                    Advance();
                    while (index < input.Length && char.IsDigit(Peek())) {
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
                while (index < input.Length && char.IsLetterOrDigit(Peek())) {
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
                while (index < input.Length && Constants.Operators.Contains(Peek())) {
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
        tokens.Add(new Token(TokenType.EOI, "EOI", (col, line, index)));
        return tokens;
    }

    private void SkipWhitespace() {
        while (index < input.Length && char.IsWhiteSpace(Peek())) {
            if (Peek() == '\n') AdvanceLine();
            else Advance();
        }
    }

    private char Peek(int ahead = 0) => (index + ahead < input.Length) ? input[index + ahead] : '\0';

    private void Advance(int amount = 1) {
        for (int i = 0; i < amount; i++) {
            if (index < input.Length) {
                if (input[index] == '\n') {
                    AdvanceLine();
                } else {
                    col++;
                }
                index++;
            }
        }
    }

    private void AdvanceLine() {
        line++;
        col = 0;
        index++;
    }

    private bool Match(string s) {
        if (index + s.Length > input.Length) return false;
        for (int i = 0; i < s.Length; i++) {
            if (input[index + i] != s[i]) return false;
        }
        return true;
    }
}
