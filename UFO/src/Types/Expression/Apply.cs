
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Expression;

public class Apply : Expression
{

    public class ApplyContin : Expression
    {
        public override void Eval(Evaluator.Evaluator etor)
        {
            Pair args = (Pair)etor.PopObj();
            UFOObject abstrObj = etor.PopObj();
            abstrObj.Apply(etor, args);
        }
    }

    private readonly ApplyContin APPLY_CONTIN = new();

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

    public override void Eval(Evaluator.Evaluator etor)
    {
        etor.PushExpr(APPLY_CONTIN);
        etor.PushExpr(Args);
        etor.PushExpr(Abstr);
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
