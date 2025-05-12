using System.Diagnostics.CodeAnalysis;

namespace UFO.Types.Literal;

public abstract class Literal : UFOObject
{
    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        etor.PushObj(this);
    }
}
