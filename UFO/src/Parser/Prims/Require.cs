namespace UFO.Parser.Prims;

public class Require(object parser, string? message = null) : IParser
{

    private readonly object _parser = parser;
    private readonly string? _message = message;

    public bool Parse(ParserState parserState)
    {
        if (Parser.Parse(_parser, parserState))
        {
            return true;
        }
        string parserName = _parser.ToString()!;
        throw new UFOException("Parser", [
            ("Message", _message != null ? Types.Literal.String.Create(_message) : Types.Literal.String.Create("Token not found")),
            ("Parser", Types.Literal.String.Create(parserName))
        ]);
    }

    public override string ToString()
    {
        return $"Require({_parser})";
    }

}
