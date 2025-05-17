using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Expression;

public abstract class Expression(TypeId typeId) : UFOObject(typeId)
{
    public abstract override UFOObject Eval(Evaluator.Evaluator etor);

    public override int GetHashCode()
    {
        throw new NotImplementedException("Expression.GetHashCode");
    }

}
