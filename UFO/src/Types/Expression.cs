using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Expression;

public abstract class Expression : UFOObject
{
    public abstract override void Eval([NotNull] Evaluator.Evaluator etor);

    public override int GetHashCode()
    {
        throw new NotImplementedException("Expression.GetHashCode");
    }

}
