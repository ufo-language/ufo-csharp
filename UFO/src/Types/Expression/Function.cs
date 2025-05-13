using System.Text;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Function : Expression
{

    private UFOObject Name;
    public Pair Parameters { get; private set; }
    public UFOObject Body { get; private set; }
    public Function? NextRule { get; private set; }

    private Function(UFOObject name, Pair parameters, UFOObject body, Function? nextRule)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
        NextRule = nextRule;
    }

    public static Function Create(Pair parameters, UFOObject body, Function? nextRule)
    {
        return new(Nil.Create(), parameters, body, nextRule);
    }

    public static Function Create(UFOObject name, Pair parameters, UFOObject body, Function? nextRule)
    {
        return new(name, parameters, body, nextRule);
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return Closure.Create(this, etor.Env);
    }

    public void SetNextRule(Function nextRule)
    {
        NextRule = nextRule;
    }

    public override void ToString(StringBuilder sb)
    {
        Function? fun = this;
        bool firstIter = true;
        sb.Append("fun ");
        if (!ReferenceEquals(fun.Name, Nil.Create()))
        {
            fun.Name.ToString(sb);
        }
        while (fun != null)
        {
            if (firstIter) firstIter = false;
            else sb.Append(", ");
            Utils.ToString.ToStringWith(sb, fun.Parameters.EachElem(), "(", ", ", ")");
            sb.Append(" = ");
            fun.Body.ToString(sb);
            fun = fun.NextRule;
        }
    }

}
