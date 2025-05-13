using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Queue : Data
{
    public class MakeQueueContin : Expression.Expression
    {
        public override void Eval([NotNull] Evaluator.Evaluator etor)
        {
            Queue q = new();
            int nElems = etor.PopContinInt();
            for (int n=0; n<nElems; n++)
            {
                q.Enq(etor.PopObj());
            }
            etor.PushObj(q);
        }
    }

    private static readonly MakeQueueContin MAKE_QUEUE_CONTIN = new();

    private Pair First;
    private Pair Last;
    public int Count { get; private set; }

    public Queue()
    {
        First = Last = Pair.EMPTY;
    }

    public static Queue Create()
    {
        return new();
    }

    public Pair AsList()
    {
        return First;
    }

    public bool Deq(out UFOObject elem)
    {
        if (Count == 0)
        {
            elem = Nil.Create();
            return false;
        }
        elem = First.First;
        First = (Pair)First.Rest;
        Count--;
        return true;
    }

    public IEnumerable<UFOObject> EachElem()
    {
        return First.EachElem();
    }

    public void Enq([NotNull] params UFOObject[] elems)
    {
        foreach (UFOObject elem in elems)
        {
            Pair pair = Pair.Create(elem);
            if (Count == 0)
            {
                First = Last = pair;
            }
            else
            {
                Last.Rest = pair;
                Last = pair;
            }
            Count++;
        }
    }

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        etor.PushContinInt(Count);
        etor.PushExpr(MAKE_QUEUE_CONTIN);
        foreach (UFOObject elem in EachElem())
        {
            etor.PushExpr(elem);
        }
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElem(), "~[", ", ", "]");
    }

}
