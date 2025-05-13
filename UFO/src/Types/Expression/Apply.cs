
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Expression;

public class Apply : Expression
{
    public UFOObject Abstr { get; private set; }
    public Pair Args { get; private set; }

    private Apply(UFOObject abstr, Pair args)
    {
        Abstr = abstr;
        Args = args;
    }

    public static Apply Create(UFOObject abstr, Pair args)
    {
        return new Apply(abstr, args);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject abstrVal = Abstr.Eval(etor);
        return abstrVal.Apply(etor, Args);
    }

    public override void ToString(StringBuilder sb)
    {
        if (Abstr is Literal.Literal)
        {
            Abstr.ToString(sb);
        }
        else
        {
            sb.Append('(');
            Abstr.ToString(sb);
            sb.Append(')');
        }
        Utils.ToString.ToStringWith(sb, Args.EachElem(), "(", ", ", ")");
    }

}
