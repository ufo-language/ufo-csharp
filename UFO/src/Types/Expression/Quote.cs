namespace UFO.Types.Expression;

public class Quote : Expression
{
    private UFOObject _expr;

    private Quote(UFOObject expr)
        : base(TypeId.QUOTE)
    {
        _expr = expr;
    }

    public static Quote Create(object tokenObj)
    {
        return new((UFOObject)tokenObj);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return _expr;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write('\'');
        _expr.ShowOn(writer);
        writer.Write('\'');
    }

}
