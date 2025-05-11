using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Data;

public abstract class Expression : UFOObject
{
    public abstract override void Eval([NotNull] Evaluator.Evaluator etor);

    public abstract override int GetHashCode();

}
