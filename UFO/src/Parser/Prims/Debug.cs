using UFO.Lexer;

namespace UFO.Parser.Prims;

public class Debug : IParser
{

    private string? _message = null;
    private object _parser;

    public Debug(object parser)
    {
        _parser = parser;
    }

    public Debug(string message, object parser)
    {
        _message = message;
        _parser = parser;
    }

    private static int _depth = 0;

    public static void _Indent()
    {
        for (int n=0; n<_depth; n++)
        {
            Console.Write("| ");
        }
    }

    public bool Parse(ParserState parserState)
    {
        bool success;
        _Indent();
        Token token = parserState.NextToken;
        if (_message == null)
        {
            Console.WriteLine($"DEBUG: {_parser} token = {token}");
            _depth++;
            success = Parser.Parse(_parser, parserState);
            _depth--;
            _Indent();
            Console.WriteLine($"DEBUG: {_parser} returning {success}, next token = '{parserState.NextToken.Lexeme}'");
        }
        else
        {
            Console.WriteLine($"DEBUG: {_message} token = {token}");
            _depth++;
            success = Parser.Parse(_parser, parserState);
            _depth--;
            _Indent();
            Console.WriteLine($"DEBUG: {_message} returning {success}, next token = '{parserState.NextToken.Lexeme}'");
        }
        return success;
    }

    public override string ToString()
    {
        return $"Debug({_parser})";
    }

}
