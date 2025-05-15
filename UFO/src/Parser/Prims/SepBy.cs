namespace UFO.Parser.Prims;

public class SepBy(object elem, object sep) : IParser
{

    private readonly object _elemParser = elem;
    private readonly object _sepParser = sep;
    // IParser _parser = new OneOf(new Seq(new RecursionBarrier(), _elemParser, new Many(new Seq(_sepParser, new Require(_elemParser, $"{elem} after {sep}")))), new Succeed(List.EMPTY));

    public bool Parse(ParserState parserState)
    {
        // bool success = parse(one_of(seq(recursion_barrier, elem, many(seq(sep, require(elem, f"{elem} after {sep}")))), succeed([])), parser_state)
        return false;
    }

    /*
    def sep_by(elem, sep, min=0):
        def _parser(parser_state):
            success = parse(one_of(seq(recursion_barrier, elem, many(seq(sep, require(elem, f"{elem} after {sep}")))), succeed([])), parser_state)
            if not success:
                return False
            # the parser returns one of:
            # 1. []
            # 2. [a, []]
            # 3. [a, [b,...]]
            if len(parser_state.value) > 0:
                parser_state.value = [parser_state.value[0]] + parser_state.value[1]
            if len(parser_state.value) < min:
                return False
            return True
        return _parser
    */
}
