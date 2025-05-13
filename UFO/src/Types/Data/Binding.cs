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
            etor.PushExpr(Create(key, value));
        }
    }

    private static readonly MakeBindingContin MAKE_BINDING_CONTIN = new();

    public UFOObject Key { get; private set; }
    public UFOObject Value { get; set; }

    private Binding(UFOObject key, UFOObject value)
    {
        Key = key;
        Value = value;
    }

    public static Binding Create(UFOObject first, UFOObject rest)
    {
        return new(first, rest);
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        etor.PushExpr(MAKE_BINDING_CONTIN);
        etor.PushExpr(Key);
        etor.PushExpr(Value);
    }

    public override void ToString(StringBuilder sb)
    {
        Key.ToString(sb);
        sb.Append('=');
        Value.ToString(sb);
    }

}
