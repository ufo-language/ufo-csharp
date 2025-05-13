using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Set : Data
{
    public class MakeSetContin : Expression.Expression
    {
        public override void Eval([NotNull] Evaluator.Evaluator etor)
        {
            Set set = new();
            int nElems = etor.PopContinInt();
            for (int n=0; n<nElems; n++)
            {
                UFOObject elem = etor.PopObj();
                set.Add(elem);
            }
            etor.PushObj(set);
        }
    }

    private static readonly MakeSetContin MAKE_SET_CONTIN = new();

    private readonly HashSet<UFOObject> Elems;

    public int Count { get { return Elems.Count; } }

    private Set([NotNull] params UFOObject[] elems)
    {
        Elems = [];
        foreach (UFOObject elem in elems)
        {
            Elems.Add(elem);
        }
    }

    public static Set Create([NotNull] params UFOObject[] elems)
    {
        return new(elems);
    }

    public void Add([NotNull] UFOObject elem)
    {
        Elems.Add(elem);
    }

    public bool Contains([NotNull] UFOObject elem)
    {
        return Elems.Contains(elem);
    }

    public IEnumerable<UFOObject> EachElem()
    {
        foreach (UFOObject elem in Elems)
        {
            yield return elem;
        }
        yield break;
    }

    public override void Eval([NotNull] Evaluator.Evaluator etor)
    {
        etor.PushContinInt(Count);
        etor.PushExpr(MAKE_SET_CONTIN);
        foreach (UFOObject elem in Elems)
        {
            etor.PushExpr(elem);
        }
    }

    public bool Remove([NotNull] UFOObject elem)
    {
        return Elems.Remove(elem);
    }

    public override void ToString(StringBuilder sb)
    {
        Utils.ToString.ToStringWith(sb, EachElem(), "${", ", ", "}");
    }

}
