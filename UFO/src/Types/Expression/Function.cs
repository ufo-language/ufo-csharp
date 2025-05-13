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

#if FALSE
    Object* closure(Object* const self, Object* const env) {
        std::size_t savedIndex = Environment::size(env);
        Object* function = self;
        while (function != GLOBALS.Nil) {
            Object* paramList = *Obj::dataLoc(function, 0);
            while (!List::isEmpty(paramList)) {
                Object* param = List::first(paramList);
                Environment::bind(env, param, param);
                paramList = List::rest(paramList);
                assert(Obj::isA(paramList, TypeId::D_LIST));
            }
            Object* closedBody = Obj::closure(*Obj::dataLoc(function, 1), env);
            *Obj::dataLoc(function, 3) = closedBody;
            Obj::setType(function, TypeId::D_CLOSURE);
            function = *Obj::dataLoc(function, 2);
        }
        Environment::setSize(env, savedIndex);
        return self;
    }
#endif

    public override void Eval(Evaluator.Evaluator etor)
    {
        // -> Pre-bind parameter variables to themselves
        //    That means a FreeVars() function is needed.
        //    Or is it just Vars()? Do they need to be free?
        // -> Close the body.
        // -> Create a new function with the new closed body.
        //    It should keep a reference to the un-closed body for display purposes.
        throw new NotImplementedException();
        /* C++:
        Object* env = VM::environment(vm);
        Function::closure(self, env);
        VM::pushObj(vm, self);
        */
    }

    public void SetNextRule(Function nextRule)
    {
        NextRule = nextRule;
    }

    public override void ToString(StringBuilder sb)
    {
        sb.Append("fun ");
        sb.Append("...");
    }

}
