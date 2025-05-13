using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Data;

public abstract class Data : UFOObject
{
    public abstract override void Eval([NotNull] Evaluator.Evaluator etor);

    public override int GetHashCode()
    {
        throw new NotImplementedException("GetHashCode");
    }

}
