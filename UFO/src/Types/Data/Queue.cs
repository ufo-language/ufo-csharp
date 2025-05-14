using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Queue : Data
{
    private List First;
    private List Last;
    public int Count { get; private set; }

    public Queue()
    {
        First = Last = List.EMPTY;
    }

    public static Queue Create()
    {
        return new();
    }

    public List AsList()
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
        First = (List)First.Rest;
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
            List List = List.Create(elem);
            if (Count == 0)
            {
                First = Last = List;
            }
            else
            {
                Last.Rest = List;
                Last = List;
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

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "~[", ", ", "]");
    }

}
