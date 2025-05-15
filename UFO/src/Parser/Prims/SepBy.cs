using UFO.Types.Data;

namespace UFO.Parser.Prims;

public class SepBy(object elem, object sep) : IParser
{

    private readonly object _elemParser = elem;
    private readonly object _sepParser = sep;

    public bool Parse(ParserState parserState)
    {
        List elems = [];
        if (Parser.Parse(_elemParser, parserState))
        {
            elems.Add(parserState.Value);
            while (Parser.Parse(_sepParser, parserState))
            {
                if (!Parser.Parse(_elemParser, parserState))
                {
                    throw new Exception($"{_elemParser} expected after {_sepParser}");
                }
                elems.Add(parserState.Value);
            }
        }
        parserState.Value = elems;
        return true;
    }

    public override string ToString()
    {
        return $"SepBy({_elemParser}, {_sepParser})";
    }

}
