using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Binding : Data
{

    public class MakeBindingContin : Expression.Expression
    {
        public override void Eval(Evaluator.Evaluator etor)
        {
            UFOObject key = etor.PopObj();
            UFOObject value = etor.PopObj();
            Binding next = (Binding)etor.PopObj();
            etor.PushExpr(Create(key, value, next));
        }
    }

    private static readonly MakeBindingContin MAKE_BINDING_CONTIN = new();
    private static readonly Binding EMPTY = new(Nil.Create(), Nil.Create(), null);

    public UFOObject Key { get; private set; }
    public UFOObject Value { get; set; }
    public Binding? Next { get; set; }


    private Binding(UFOObject key, UFOObject value, Binding? next)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public static Binding Create()
    {
        return EMPTY;
    }

    public static Binding Create(UFOObject key, UFOObject value)
    {
        return new(key, value, EMPTY);
    }

    public static Binding Create(UFOObject key, UFOObject value, Binding next)
    {
        return new(key, value, next);
    }

    public IEnumerable<Binding> EachElem()
    {
        Binding binding = this;
        while (!binding.IsEmpty())
        {
            yield return binding;
            binding = Next!;
        }
        yield break;
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        if (ReferenceEquals(this, EMPTY))
        {
            etor.PushObj(EMPTY);
            return;
        }
        etor.PushExpr(MAKE_BINDING_CONTIN);
        etor.PushExpr(Key);
        etor.PushExpr(Value);
        etor.PushExpr(Next!);
    }

    public bool IsEmpty()
    {
        return ReferenceEquals(this, EMPTY);
    }

    public Binding Locate(UFOObject key)
    {
        foreach (Binding binding in EachElem())
        {
            if (key.Equals(binding.Key))
            {
                return binding;
            }
        }
        return EMPTY;
    }

    public override void ToString(StringBuilder sb)
    {
        Key.ToString(sb);
        sb.Append('=');
        Value.ToString(sb);
    }

}
