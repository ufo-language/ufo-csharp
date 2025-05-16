using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class IfThen(UFOObject cond, UFOObject conseq, UFOObject alt) : Expression
{
    private readonly UFOObject _cond = cond;
    private readonly UFOObject _conseq = conseq;
    private readonly UFOObject _alt = alt;

    public static IfThen Create(Parser.List elems)
    {
        UFOObject cond = (UFOObject)elems[0];
        UFOObject conseq = (UFOObject)elems[1];
        UFOObject alt = elems.Count == 3 ? (UFOObject)elems[2] : Nil.NIL;
        return new(cond, conseq, alt);
    }

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
        if (_alt != Nil.NIL)
        {
            writer.Write(" else ");
            _alt.ShowOn(writer);
        }
    }

}
