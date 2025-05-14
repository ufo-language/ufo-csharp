using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Seq : Expression
{
    private readonly UFOObject[] _exprs;

    private Seq(params UFOObject[] exprs)
    {
        _exprs = exprs;
    }

    public static Seq Create(params UFOObject[] exprs)
    {
        return new Seq(exprs);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject returnValue = Nil.Create();
        foreach (UFOObject expr in _exprs)
        {
            returnValue = expr.Eval(etor);
        }
        return returnValue;
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, _exprs, "(", "; ", ")");
    }

}
