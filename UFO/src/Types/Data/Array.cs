using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Array : Data
{

    private readonly List<UFOObject> _elems;

    public int Count { get { return _elems.Count; } }

    private Array(params UFOObject[] elems)
    {
        _elems = [.. elems];
    }

    public static Array Create(params UFOObject[] elems)
    {
        return new(elems);
    }

    public void Append(UFOObject elem)
    {
        _elems.Add(elem);
    }

    public override bool BoolValue => _elems.Count > 0;

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
        Array newArray = new();
        foreach (UFOObject elem in EachElem())
        {
            UFOObject value = elem.Eval(etor);
            newArray.Append(value);
        }
        return newArray;
    }

    public bool Get(int index, out UFOObject elem)
    {
        elem = Nil.Create();
        if (index < 0 || index >= Count)
        {
            return false;
        }
        elem = _elems[index];
        return true;
    }

    public UFOObject Get_Unsafe(int index)
    {
        return _elems[index];
    }

    public bool Set(int index, UFOObject elem)
    {
        if (index < 0 || index >= Count)
        {
            return false;
        }
        _elems[index] = elem;
        return true;
    }

    public void Set_Unsafe(int index, UFOObject elem)
    {
        _elems[index] = elem;
    }

    public override void ShowOn(TextWriter writer)
    {
        Utils.ShowOn.ShowOnWith(writer, EachElem(), "{", ", ", "}");
    }

}
