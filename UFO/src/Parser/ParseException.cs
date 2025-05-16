using System.Text;
using UFO.Lexer;

namespace UFO.Parser;

public class ParseException(string message, ParserState parserState) : Exception
{

    private string _message = message;
    private ParserState _parserState = parserState;

    private readonly string _PREFIX = "==> ";

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(_PREFIX);
        sb.Append("Parse exception: ");
        sb.Append(_message);
        sb.Append('\n');
        sb.Append(_PREFIX);
        int indentSpaces = 0;
        for (int n=0; n<_parserState.Tokens.Count - 1; n++)
        {
            Token token = _parserState.Tokens[n];
            sb.Append($"{token.Lexeme} ");
            if (n < _parserState.TokenIndex)
            {
                indentSpaces += token.Lexeme.Length + 1;
            }
        }
        sb.Append('\n');
        sb.Append(_PREFIX);
        sb.Append(new string(' ', indentSpaces));
        sb.Append("^\n");
        return sb.ToString();
    }

}
