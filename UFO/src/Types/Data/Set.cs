using System.Diagnostics.CodeAnalysis;
using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Set : Data
{
    private readonly HashSet<UFOObject> _elems;

    public int Count { get { return _elems.Count; } }

    private Set(params UFOObject[] elems)
    {
        _elems = [];
        foreach (UFOObject elem in elems)
        {
            _elems.Add(elem);
        }
    }

    public static Set Create(params UFOObject[] elems)
    {
        return new(elems);
    }

    public void Add(UFOObject elem)
    {
        _elems.Add(elem);
    }

    public bool Contains(UFOObject elem)
    {
        return _elems.Contains(elem);
    }

    public IEnumerable<UFOObject> EachElem()
    {
        foreach (UFOObject elem in _elems)
        {
            yield return elem;
        }
        yield break;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        Set newSet = new();
        foreach (UFOObject elem in _elems)
        {
            newSet.Add(elem.Eval(etor));
        }
        return newSet;
    }

    public bool Remove(UFOObject elem)
    {
        return _elems.Remove(elem);
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "${", ", ", "}");
    }

}
