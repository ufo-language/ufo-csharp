using System.Text;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Types.Literal;

public class Closure : Literal
{

    public class ArgumentMismatchException : Exception
    {
        public Function Fun { get; private set; }
        public List Args { get; private set; }
        public ArgumentMismatchException(Function fun, List args)
        {
            Fun = fun;
            Args = args;
        }
    }

    public Function Fun { get; private set; }
    public Binding LexicalEnv { get; private set; }

    private Closure(Function function, Binding lexicalEnv)
    {
        Fun = function;
        LexicalEnv = lexicalEnv;
    }

    public override UFOObject Apply(Evaluator.Evaluator etor, List args)
    {
        List argValues = (List)args.Eval(etor);
        Binding env = etor.Env;
        Function? fun = Fun;
        while (fun != null)
        {
            List parameters = fun.Parameters;
            if (parameters.Match(args, ref etor))
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
