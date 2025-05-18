using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Queue : Data
{
    private List _first;
    private List _last;
    public int Count { get; private set; }

    private Queue()
        : base(TypeId.QUEUE)
    {
        _first = _last = List.EMPTY;
    }

    public static Queue Create()
    {
        return new();
    }

    public static Queue Create(Parser.List elems)
    {
        Queue q = new();
        foreach (object elem in elems)
        {
            q.Enq((UFOObject)elem);
        }
        return q;
    }

    public List AsList()
    {
        return _first;
    }

    public override bool BoolValue => !IsEmpty;

    public bool Deq(out UFOObject elem)
    {
        if (Count == 0)
        {
            elem = Nil.Create();
            return false;
        }
        elem = _first.First;
        _first = (List)_first.Rest;
        Count--;
        return true;
    }

    public IEnumerable<UFOObject> EachElem()
    {
        return _first.EachElem();
    }

    public void Enq(params UFOObject[] elems)
    {
        foreach (UFOObject elem in elems)
        {
            List List = List.Create(elem);
            if (Count == 0)
            {
                _first = _last = List;
            }
            else
            {
                _last.Rest = List;
                _last = List;
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

    public bool IsEmpty => _first.IsEmpty;

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "~[", ", ", "]");
    }
}
