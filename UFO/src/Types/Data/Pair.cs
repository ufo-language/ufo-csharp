using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Pair : Data
{

    public class MakePairContin : Expression.Expression
    {
        public override void Eval([NotNull] Evaluator.Evaluator etor)
        {
            UFOObject condVal = etor.PopObj();
            UFOObject altExpr = etor.PopObj();
            UFOObject conseqExpr = etor.PopObj();
            etor.PushExpr(condVal.BoolValue() ? conseqExpr : altExpr);
        }

        public override string ToString()
        {
            return GetType().Name + "{}";
        }
    }

    private static readonly MakePairContin MAKE_PAIR_CONTIN = new();

    public static readonly Pair EMPTY = new(Nil.Create(), Nil.Create());

    public UFOObject First { get; set; }
    public UFOObject Rest { get; set; }

    private Pair(UFOObject first, UFOObject rest)
    {
        First = first;
        Rest = rest;
    }

    public static Pair Create()
    {
        return EMPTY;
    }

    public static Pair Create([NotNull] UFOObject first)
    {
        return new(first, EMPTY);
    }

    public static Pair Create([NotNull] UFOObject first, [NotNull] UFOObject rest)
    {
        return new(first, rest);
    }

    // public static Pair Create(params UFOObject[] args)
    // {
    //     Queue q = Queue.Create();
    //     foreach (UFOObject arg in args)
    //     {
    //         q.Enq(arg);
    //     }
    //     return q.AsList();
    // }

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        etor.PushExpr(MAKE_PAIR_CONTIN);
        etor.PushExpr(Rest);
        etor.PushExpr(First);
    }

    public IEnumerable<UFOObject> EachElem()
    {
        Pair pair = this;
        while (!pair.IsEmpty())
        {
            yield return pair.First;
            UFOObject restObject = pair.Rest;
            if (restObject.GetType() != typeof(Pair))
            {
                pair = Create(restObject);
            }
            else
            {
                pair = (Pair)restObject;
            }
        }
        yield break;
    }

    public bool IsEmpty()
    {
        return ReferenceEquals(this, EMPTY);
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElem(), "[", ", ", "]");
    }

}
