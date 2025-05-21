

namespace UFO.Types.Expression;

public class ScopeResolution : Expression
{
    private List<UFOObject> _segments = [];

    private ScopeResolution()
        : base(TypeId.SCOPE_RESOLUTION)
    { }

    public static ScopeResolution Create(Parser.List parts)
    {
        ScopeResolution scopeRes = new();
        foreach (object part in parts)
        {
            scopeRes._segments.Add((UFOObject)part);
        }
        return scopeRes;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject obj = _segments[0].Eval(etor);
        for (int n = 1; n < _segments.Count; n++)
        {
            UFOObject obj1;
            if (!obj.Get(_segments[n], out obj1))
            {
                throw new UFOException("MissingScope", [
                    ("Message", Literal.String.Create("Scope resolution operator could not find segment in collection.")),
                    ("ScopeResolutionOperator", this),
                    ("Segment", _segments[n]),
                    ("Collection", obj)
                ]);
                throw new Exception($"ScopeResolutionOperator could not find {_segments[n]} in {obj}");
            }
            obj = obj1;
        }
        return obj;
    }

    public override void ShowOn(TextWriter writer)
    {
        string s = string.Join(":", _segments);
        writer.Write(s);
    }
}