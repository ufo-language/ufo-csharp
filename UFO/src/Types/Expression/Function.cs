using UFO.Types.Literal;

namespace UFO.Types.Expression;

public class Function : Expression
{

    private readonly UFOObject _name;
    public List<UFOObject> Parameters { get; private set; }
    public UFOObject Body { get; private set; }
    public Function? NextRule { get; private set; }

    private Function(UFOObject name, List<UFOObject> parameters, UFOObject body, Function? nextRule)
        : base(TypeId.FUNCTION)
    {
        _name = name;
        Parameters = parameters;
        Body = body;
        NextRule = nextRule;
    }

    public static Function Create(List<UFOObject> parameters, UFOObject body)
    {
        return new(Nil.Create(), parameters, body, null);
    }

    public static Function Create(List<UFOObject> parameters, UFOObject body, Function nextRule)
    {
        return new(Nil.Create(), parameters, body, nextRule);
    }

    public static Function Create(UFOObject name, List<UFOObject> parameters, UFOObject body)
    {
        return new(name, parameters, body, null);
    }
    
    public static Function Create(UFOObject name, List<UFOObject> parameters, UFOObject body, Function nextRule)
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

    public override void ShowOn(TextWriter writer)
    {
        Function? fun = this;
        bool firstIter = true;
        writer.Write("fun ");
        if (!ReferenceEquals(fun._name, Nil.Create()))
        {
            fun._name.ShowOn(writer);
        }
        while (fun != null)
        {
            if (firstIter) firstIter = false;
            else writer.Write(", ");
            Utils.ShowOn.ShowOnWith(writer, fun.Parameters, "(", ", ", ")");
            writer.Write(" = ");
            fun.Body.ShowOn(writer);
            fun = fun.NextRule;
        }
    }
}
