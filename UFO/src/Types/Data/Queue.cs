using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Queue : Data
{
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

    public void Enq(params UFOObject[] elems)
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

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        Queue newQueue = new();
        foreach (UFOObject elem in EachElem())
        {
            newQueue.Enq(elem.Eval(etor));
        }
        return newQueue;
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElem(), "~[", ", ", "]");
    }

}
