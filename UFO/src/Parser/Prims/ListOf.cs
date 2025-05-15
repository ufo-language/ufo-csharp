namespace UFO.Parser.Prims;

public class ListOf
{

    private readonly object _openParser;
    private readonly object _elemParser;
    private readonly object _sepParser;
    private readonly object _closeParser;
    private readonly object? _barParser;

    public ListOf(object open, object elem, object sep, object close)
    {
        _openParser = open;
        _elemParser = elem;
        _sepParser = sep;
        _closeParser = close;
        _barParser = null;
    }

    public ListOf(object open, object elem, object sep, object close, object bar)
    {
        _openParser = open;
        _elemParser = elem;
        _sepParser = sep;
        _closeParser = close;
        _barParser = bar;
    }

    /*
    def list_of(open, elem, sep, close, bar=None):
        def _proper_list(parser_state):
            parser = seq(open, sep_by(elem, sep), require(close))
            success = parse(parser, parser_state)
            return success
        def _improper_list(parser_state):
            parser = seq(open, maybe(seq(sep_by(elem, sep), one_of(seq(bar, elem), succeed(None)))), close)
            success = parse(parser, parser_state)
            return success
        return _proper_list if bar is None else _improper_list
    */

    public override string ToString()
    {
        return $"ListOf({_openParser}, {_elemParser}, {_sepParser}, {_closeParser}, {_barParser})";
    }
}
