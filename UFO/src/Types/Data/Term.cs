
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Term : Data
{
    public Symbol Name { get; private set; }
    public Array Args { get; private set; }
    public UFOObject Attrib;

    private Term(Symbol name, Array args, UFOObject attrib)
        : base(TypeId.TERM)
    {
        Name = name;
        Args = args;
        Attrib = attrib;
    }

    public static Term Create(Symbol name, params UFOObject[] args)
    {
        return new Term(name, Array.Create(args), Nil.Create());
    }

    public static Term Create(Parser.List elems)
    {
        Symbol name = (Symbol)elems[0];
        Array args = (Array)elems[1];
        UFOObject attrib = elems.Count == 3 ? (UFOObject)elems[2] : Nil.NIL;
        return new(name, args, attrib);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return new Term(Name, (Array)Args.Eval(etor), Attrib.Eval(etor));
    }

    public override void ShowOn(TextWriter writer)
    {
        Name.ShowOn(writer);
        Args.ShowOn(writer);
    }

}
