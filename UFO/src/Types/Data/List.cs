using UFO.Types.Literal;

namespace UFO.Types.Data;

public class List : Data
{
    public static readonly List EMPTY = new(Nil.Create(), Nil.Create());
    public UFOObject First { get; set; }
    public UFOObject Rest { get; set; }

    private List(UFOObject first, UFOObject rest)
        : base(TypeId.LIST)
    {
        First = first;
        Rest = rest;
    }

    public static List Create()
    {
        return EMPTY;
    }

    public static List Create(UFOObject first)
    {
        return new(first, EMPTY);
    }

    public static List Create(UFOObject first, UFOObject rest)
    {
        return new(first, rest);
    }

    public static List Create(params UFOObject[] args)
    {
        Queue q = Queue.Create();
        foreach (UFOObject arg in args)
        {
            q.Enq(arg);
        }
        return q.AsList();
    }

    public static List Create(Parser.List elems)
    {
        Queue q = Queue.Create();
        foreach (object elem in elems)
        {
            q.Enq((UFOObject)elem);
        }
        return q.AsList();
    }

    public override bool BoolValue => !IsEmpty;

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return IsEmpty ? this : new List(First.Eval(etor), Rest.Eval(etor));
    }

    public IEnumerable<UFOObject> EachElem()
    {
        List List = this;
        while (!List.IsEmpty)
        {
            yield return List.First;
            UFOObject restObject = List.Rest;
            if (restObject.GetType() != typeof(List))
            {
                List = Create(restObject);
            }
            else
            {
                List = (List)restObject;
            }
        }
        yield break;
    }

    public bool IsEmpty => ReferenceEquals(this, EMPTY);

    public override bool Match(UFOObject other, ref Evaluator.Evaluator etor)
    {
        if (GetType() != other.GetType())
        {
            return false;
        }
        List otherList = (List)other;
        return First.Match(otherList.First, ref etor) && Rest.Match(otherList.Rest, ref etor);
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "[", ", ", "]");
    }
}
