using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Swift;

namespace UFO.Types.Literal;

public abstract class Literal : UFOObject
{
    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return this;
    }
}
