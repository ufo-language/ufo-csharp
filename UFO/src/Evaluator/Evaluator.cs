using System.Linq.Expressions;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Evaluator;

public class Evaluator
{
    private readonly Stack<UFOObject> OStack;
    private readonly Stack<UFOObject> EStack;
    private readonly Stack<int> ContinIntStack;
    private Binding Env;

    public Evaluator()
    {
        OStack = new();
        EStack = new();
        ContinIntStack = new();
        Env = Binding.Create();
    }

    public void Bind(Identifier name, UFOObject value)
    {
        Env = Binding.Create(name, value, Env);
    }

    public bool Lookup_Rel(Identifier name, ref UFOObject value)
    {
        value = default!;
        Binding binding = Env.Locate(name);
        if (binding.IsEmpty()) {
            return false;
        }
        value = binding.Value;
        return true;
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

    public void PushContinInt(int n)
    {
        ContinIntStack.Push(n);
    }

    public int PopContinInt()
    {
        return ContinIntStack.Pop();
    }

    public void Run()
    {
        while(EStack.Count > 0)
        {
            Step();
        }
    }
    
    public void Step()
    {
        UFOObject expr = PopExpr();
        // Console.WriteLine($"Evaluator.Step got expr {expr}");
        expr.Eval(this);
    }

}
