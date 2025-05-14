
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Expression;

public class Apply : Expression
{
    public UFOObject Abstr { get; private set; }
    public List Args { get; private set; }

    private Apply(UFOObject abstr, List args)
    {
        Abstr = abstr;
        Args = args;
    }

    public static Apply Create(UFOObject abstr, List args)
    {
        return new Apply(abstr, args);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject abstrVal = Abstr.Eval(etor);
        return abstrVal.Apply(etor, Args);
    }

    public override void ShowOn(TextWriter writer)
    {
        if (Abstr is Literal.Literal)
        {
            Abstr.ShowOn(writer);
        }
        else
        {
            writer.Write('(');
            Abstr.ShowOn(writer);
            writer.Write(')');
        }
        Utils.ShowOn.ShowOnWith(writer, Args.EachElem(), "(", ", ", ")");
    }

}
