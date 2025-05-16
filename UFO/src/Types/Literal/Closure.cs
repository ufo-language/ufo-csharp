using System.Text;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Types.Literal;

public class Closure : Literal
{

    public class ArgumentMismatchException(Function fun, List<UFOObject> args) : Exception
    {
        public Function Fun { get; private set; } = fun;
        public List<UFOObject> Args { get; private set; } = args;
    }

    public Function Fun { get; private set; }
    public Binding LexicalEnv { get; private set; }

    private Closure(Function function, Binding lexicalEnv)
    {
        Fun = function;
        LexicalEnv = lexicalEnv;
    }

    public override UFOObject Apply(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        List<UFOObject> argValues = etor.EvalEach(args);
        Binding env = etor.Env;
        Function? fun = Fun;
        while (fun != null)
        {
            if (UFOObject.Match(fun.Parameters, args, etor))
            {
                return fun.Body.Eval(etor);
            }
            fun = fun.NextRule;
        }
        throw new ArgumentMismatchException(Fun, args);
    }

    public static Closure Create(Function function, Binding lexicalEnv)
    {
        return new(function, lexicalEnv);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public override void ShowOn(TextWriter writer)
    {
        Fun.ShowOn(writer);
    }

}
