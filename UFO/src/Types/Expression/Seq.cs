using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Seq : Expression
{
    private readonly UFOObject[] Exprs;

    private Seq(params UFOObject[] exprs)
    {
        Exprs = exprs;
    }

    public static Seq Create(params UFOObject[] exprs)
    {
        return new Seq(exprs);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject returnValue = Nil.Create();
        foreach (UFOObject expr in Exprs)
        {
            returnValue = expr.Eval(etor);
        }
        return returnValue;
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, Exprs, "(", "; ", ")");
    }

}
