using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Expression;

// This is technically a Data class because it evalautes to the same type,
// but it is not a container data type.
public class Function : Expression
{

    private readonly Pair Parameters;
    private readonly UFOObject Body;
    private Function? NextRule;

    private Function(Pair parameters, UFOObject body, Function? nextRule)
    {
        Parameters = parameters;
        Body = body;
        NextRule = nextRule;
    }

    public static Function Create(Pair parameters, UFOObject body, Function? nextRule)
    {
        return new(parameters, body, nextRule);
    }

    public void SetNextRule(Function nextRule)
    {
        NextRule = nextRule;
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        throw new NotImplementedException();
    }

    public override void ToString(StringBuilder sb)
    {
        sb.Append("fun ");
        sb.Append("...");
    }

}
