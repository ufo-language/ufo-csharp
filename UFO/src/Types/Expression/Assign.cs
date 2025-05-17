
namespace UFO.Types.Expression;

public class Assign : Expression
{
    private UFOObject _lhs;
    private UFOObject _rhs;

    private Assign(UFOObject lhs, UFOObject rhs)
        : base(TypeId.ASSIGN)
    {
        _lhs = lhs;
        _rhs = rhs;
    }

    public static Assign Create(Parser.List exprs)
    {
        return new((UFOObject)exprs[0], (UFOObject)exprs[1]);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        UFOObject rhsVal = _rhs.Eval(etor);
        if (!_lhs.Match(rhsVal, ref etor))
        {
            throw new Exception($"Match failure: {_lhs} := {rhsVal}");
        }
        return rhsVal;
    }
}
