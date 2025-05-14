using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types.Expression;

public class IfThen(UFOObject cond, UFOObject conseq, UFOObject alt) : Expression
{
    private readonly UFOObject _cond = cond;
    private readonly UFOObject _conseq = conseq;
    private readonly UFOObject _alt = alt;

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return (_cond.Eval(etor).BoolValue ? _conseq : _alt).Eval(etor);
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write("if ");
        _cond.ShowOn(writer);
        writer.Write(" then ");
        _conseq.ShowOn(writer);
        writer.Write(" else ");
        _alt.ShowOn(writer);
    }

}
