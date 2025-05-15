namespace UFO.Parser.Prims;

public class ListOf : IParser
{

    private readonly object _openParser;
    private readonly object _elemParser;
    private readonly object _sepParser;
    private readonly object _closeParser;
    private readonly object? _barParser;
    private readonly object _sepByParser;

    public ListOf(object open, object elem, object sep, object close)
    {
        _openParser = open;
        _elemParser = elem;
        _sepParser = sep;
        _closeParser = close;
        _barParser = null;
        _sepByParser = new SepBy(_elemParser, _sepParser);
    }

    public ListOf(object open, object elem, object sep, object close, object bar)
        : this(open, elem, sep, close)
    {
        _barParser = bar;
    }

    private bool ParseProperList(ParserState parserState)
    {
        if (!Parser.Parse(_openParser, parserState)) {
            return false;
        }
        if (!Parser.Parse(_sepByParser, parserState)) {
            throw new Exception($"Element {_elemParser} expected after opening {_openParser}");
        }
        List elems = (List)parserState.Value;
        if (!Parser.Parse(_closeParser, parserState)) {
            throw new ParseException($"Closing '{_closeParser}' expected after opening '{_openParser}' or element '{_elemParser}'", parserState);
        }
        parserState.Value = elems;
        return true;
    }

    private bool ParseImproperList(ParserState parserState)
    {
        // TODO implement this correctly instead of calling ParseProperList
        return ParseProperList(parserState);
    }

    public bool Parse(ParserState parserState)
    {
        return _barParser == null ? ParseProperList(parserState) : ParseImproperList(parserState);
    }

    public override string ToString()
    {
        return $"ListOf('{_openParser}', '{_elemParser}', '{_sepParser}', '{_closeParser}', '{_barParser}')";
    }
}
