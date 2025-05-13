using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Expression;

public class IfThen(UFOObject cond, UFOObject conseq, UFOObject alt) : Expression
{
    private readonly UFOObject Cond = cond;
    private readonly UFOObject Conseq = conseq;
    private readonly UFOObject Alt = alt;

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return (Cond.Eval(etor).BoolValue() ? Conseq : Alt).Eval(etor);
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
