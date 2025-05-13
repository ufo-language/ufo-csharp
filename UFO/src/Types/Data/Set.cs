using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Set : Data
{
    private readonly HashSet<UFOObject> Elems;

    public int Count { get { return Elems.Count; } }

    private Set(params UFOObject[] elems)
    {
        Elems = [];
        foreach (UFOObject elem in elems)
        {
            Elems.Add(elem);
        }
    }

    public static Set Create(params UFOObject[] elems)
    {
        return new(elems);
    }

    public void Add(UFOObject elem)
    {
        Elems.Add(elem);
    }

    public bool Contains(UFOObject elem)
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

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        Set newSet = new();
        foreach (UFOObject elem in Elems)
        {
            newSet.Add(elem.Eval(etor));
        }
        return newSet;
    }

    public bool Remove(UFOObject elem)
    {
        return Elems.Remove(elem);
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "${", ", ", "}");
    }

}
