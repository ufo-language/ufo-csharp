using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Evaluator;

public class Evaluator
{
    private readonly Stack<UFOObject> OStack;
    private readonly Stack<UFOObject> EStack;
    private readonly Environment Env;

    public Evaluator()
    {
        OStack = new Stack<UFOObject>();
        EStack = new Stack<UFOObject>();
        Env = new Environment();
    }

    public void Bind(Identifier name, UFOObject value)
    {
        Env.Bind(name, value);
    }

    public bool Lookup_Rel(Identifier name, ref UFOObject value)
    {
        value = default!;
        return Env.Lookup_Rel(name, ref value);
    }

    public void PushObj(UFOObject obj)
    {
        OStack.Push(obj);
    }

    public UFOObject PopObj()
    {
        return OStack.Pop();
    }

    public void PushExpr(UFOObject obj)
    {
        EStack.Push(obj);
    }

    public UFOObject PopExpr()
    {
        return EStack.Pop();
    }
    
}
