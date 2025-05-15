
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Term : Data
{
    public Symbol Name { get; private set; }
    public Array Args { get; private set; }
    public UFOObject Attrib;

    private Term(Symbol name, Array args, UFOObject attrib)
    {
        Name = name;
        Args = args;
        Attrib = attrib;
    }

    public static Term Create(Parser.List elems)
    {
        Console.WriteLine($"Term.Create elems = {elems}");
        Symbol name = (Symbol)elems[0];
        Array args = (Array)elems[1];
        Console.WriteLine($"  name = '{name}', args = '{args}'");
        UFOObject attrib = elems.Count == 3 ? (UFOObject)elems[2] : Nil.NIL;
        Term term = new(name, args, attrib);
        Console.WriteLine($"  Term = {term}");
        return term;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return new Term(Name, (Array)Args.Eval(etor), Attrib.Eval(etor));
    }

    public override void ShowOn(TextWriter writer)
    {
        Name.ShowOn(writer);
        Args.ShowOn(writer);
        if (Attrib != Nil.NIL)
        {
            Attrib.ShowOn(writer);
        }
    }

}
