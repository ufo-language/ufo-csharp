using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Expression;

public class Seq(params UFOObject[] exprs) : Expression
{

    public class DropContin : Expression
    {
        public override void Eval([NotNull] Evaluator.Evaluator etor)
        {
            etor.PopObj();
        }

        public override string ToString()
        {
            return "DropContin{}";
        }
    }

    private static readonly DropContin DROP_CONTIN = new();

    private readonly UFOObject[] Exprs = exprs;

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
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

    public override string ToString()
    {
        string s = "(";
        bool firstIter = true;
        foreach (UFOObject expr in Exprs)
        {
            if (firstIter)
                firstIter = false;
            else
                s += "; ";
            s += expr;
        }
        s += ")";
        return s;
    }

}
