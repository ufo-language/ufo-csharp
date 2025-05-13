using System.ComponentModel;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Types.Literal;

public class Closure : Literal
{

    public class ArgumentMismatchException : Exception
    {
        public Function Fun { get; private set; }
        public Pair Args { get; private set; }
        public ArgumentMismatchException(Function fun, Pair args)
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

    public override void Apply(Evaluator.Evaluator etor, Pair args)
    {
        Binding env = etor.Env;
        Function? fun = Fun;
        while (fun != null)
        {
            Pair parameters = fun.Parameters;
            if (parameters.Match(args, ref env))
            {
                etor.PushExpr(fun.Body, env);
            }
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

}
