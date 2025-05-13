using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Array : Data
{
    public class MakeArrayContin : Expression.Expression
    {
        public override void Eval(Evaluator.Evaluator etor)
        {
            Array ary = new();
            int nElems = etor.PopContinInt();
            for (int n=0; n<nElems; n++)
            {
                ary.Add(etor.PopObj());
            }
            etor.PushObj(ary);
        }
    }

    private static readonly MakeArrayContin MAKE_ARRAY_CONTIN = new();

    private readonly List<UFOObject> Elems;

    public int Count { get { return Elems.Count; } }

    private Array(params UFOObject[] elems)
    {
        Elems = [.. elems];
    }

    public static Array Create(params UFOObject[] elems)
    {
        return new(elems);
    }

    public void Add(UFOObject elem)
    {
        Elems.Add(elem);
    }

    public IEnumerable<UFOObject> EachElem()
    {
        foreach (UFOObject elem in Elems)
        {
            yield return elem;
        }
        yield break;
    }

    public override void Eval(Evaluator.Evaluator etor)
    {
        etor.PushContinInt(Count);
        etor.PushExpr(MAKE_ARRAY_CONTIN);
        foreach (UFOObject elem in EachElem())
        {
            etor.PushExpr(elem);
        }
    }

    public bool Get(int index, out UFOObject elem)
    {
        elem = Nil.Create();
        if (index < 0 || index >= Count)
        {
            return false;
        }
        elem = Elems[index];
        return true;
    }

    public UFOObject Get_Unsafe(int index)
    {
        return Elems[index];
    }

    public bool Set(int index, UFOObject elem)
    {
        if (index < 0 || index >= Count)
        {
            return false;
        }
        Elems[index] = elem;
        return true;
    }

    public void Set_Unsafe(int index, UFOObject elem)
    {
        Elems[index] = elem;
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElem(), "{", ", ", "}");
    }

}
