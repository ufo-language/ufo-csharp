using UFO.Types.Data;

namespace UFO.Parser.Prims;

public class SepBy(object elem, object sep, int min=0) : IParser
{

    private readonly object _elemParser = elem;
    private readonly object _sepParser = sep;
    private int _min = min;

    public bool Parse(ParserState parserState)
    {
        List elems = [];
        if (Parser.Parse(_elemParser, parserState))
        {
            elems.Add(parserState.Value);
            // Console.WriteLine($"==== SepBy looking for {_sepParser}, nextToken = {parserState.NextToken}");
            while (Parser.Parse(_sepParser, parserState))
            {
                // Console.WriteLine($"==== SepBy found {_sepParser}, looking for {_elemParser}, nextToken = {parserState.NextToken}");
                if (!Parser.Parse(_elemParser, parserState))
                {
                    // Console.WriteLine($"==== SepBy found separator {_sepParser} but no element {_elemParser}");
                    string lexeme = parserState.NextToken.Lexeme;
                    throw new Exception($"'{_elemParser}' expected after '{_sepParser}', found '{lexeme}'");
                }
                elems.Add(parserState.Value);
            }
            // Console.WriteLine($"==== SepBy did not find separator, returning {elems}");
        }
        if (elems.Count < _min)
        {
            return false;
        }
        parserState.Value = elems;
        return true;
    }

    public override string ToString()
    {
        return $"SepBy({_elemParser}, {_sepParser})";
    }

}
