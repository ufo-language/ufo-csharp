using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Expression;

public class IfThen(UFOObject cond, UFOObject conseq, UFOObject alt) : Expression
{

    public class SelectContin : Expression
    {
        public override void Eval(Evaluator.Evaluator etor)
        {
            UFOObject condVal = etor.PopObj();
            UFOObject altExpr = etor.PopObj();
            UFOObject conseqExpr = etor.PopObj();
            etor.PushExpr(condVal.BoolValue() ? conseqExpr : altExpr);
        }

    }

    private static readonly SelectContin SELECT_CONTIN = new();

    private readonly UFOObject Cond = cond;
    private readonly UFOObject Conseq = conseq;
    private readonly UFOObject Alt = alt;

    public override void Eval(Evaluator.Evaluator etor)
    {
        etor.PushObj(Conseq);
        etor.PushObj(Alt);
        etor.PushExpr(SELECT_CONTIN);
        etor.PushExpr(Cond);
    }

    public override void ToString(StringBuilder sb)
    {
        sb.Append("if ");
        Cond.ToString(sb);
        sb.Append(" then ");
        Conseq.ToString(sb);
        sb.Append(" else ");
        Alt.ToString(sb);
    }

}
