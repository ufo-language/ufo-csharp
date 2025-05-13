using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Seq : Expression
{

    public class DropContin : Expression
    {
        public override void Eval(Evaluator.Evaluator etor)
        {
            etor.PopObj();
        }
    }

    private static readonly DropContin DROP_CONTIN = new();

    private readonly UFOObject[] Exprs;

    private Seq(params UFOObject[] exprs)
    {
        Exprs = exprs;
    }

    public static Seq Create(params UFOObject[] exprs)
    {
        return new Seq(exprs);
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        int nExprs = Exprs.Length;
        if (nExprs == 0) {
            etor.PushObj(Nil.Create());
            return;
        }
        bool firstIter = true;
        for (int n = Exprs.Length - 1; n>=0; n--)
        {
            if (firstIter)
                firstIter = false;
            else
                etor.PushExpr(DROP_CONTIN);
            UFOObject expr = Exprs[n];
            etor.PushExpr(expr);
        }
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, Exprs, "(", "; ", ")");
    }

}
