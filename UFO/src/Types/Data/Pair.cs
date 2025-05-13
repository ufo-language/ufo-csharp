using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Pair : Data
{
    public static readonly Pair EMPTY = new(Nil.Create(), Nil.Create());

    public UFOObject First { get; set; }
    public UFOObject Rest { get; set; }

    protected Pair(UFOObject first, UFOObject rest)
    {
        First = first;
        Rest = rest;
    }

    public static Pair Create()
    {
        return EMPTY;
    }

    public static Pair Create(UFOObject first)
    {
        return new(first, EMPTY);
    }

    public static Pair Create(UFOObject first, UFOObject rest)
    {
        return new(first, rest);
    }

    public static Pair Create(params UFOObject[] args)
    {
        Queue q = Queue.Create();
        foreach (UFOObject arg in args)
        {
            q.Enq(arg);
        }
        return q.AsList();
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return IsEmpty() ? this : new Pair(First.Eval(etor), Rest.Eval(etor));
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
