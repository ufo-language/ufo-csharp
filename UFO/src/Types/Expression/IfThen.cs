using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Expression;

public class IfThen([NotNull] UFOObject cond, UFOObject conseq, UFOObject alt) : Expression
{

    public class SelectContin : Expression
    {
        public override void Eval([NotNull] Evaluator.Evaluator etor)
        {
            UFOObject condVal = etor.PopObj();
            UFOObject altExpr = etor.PopObj();
            UFOObject conseqExpr = etor.PopObj();
            etor.PushExpr(condVal.BoolValue() ? conseqExpr : altExpr);
        }

        public override string ToString()
        {
            return "SelectContin{}";
        }
    }

    private static readonly SelectContin SELECT_CONTIN = new();

    private readonly UFOObject Cond = cond;
    private readonly UFOObject Conseq = conseq;
    private readonly UFOObject Alt = alt;

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        etor.PushObj(Conseq);
        etor.PushObj(Alt);
        etor.PushExpr(SELECT_CONTIN);
        etor.PushExpr(Cond);
    }

    public override string ToString()
    {
        return "if " + Cond + " then " + Conseq + " else " + Alt;
    }

}
