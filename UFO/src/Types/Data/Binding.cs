using System.Text;
using UFO.Types.Literal;

namespace UFO.Types.Data;

public class Binding : Data
{
    private static readonly Binding _EMPTY = new(Nil.Create(), Nil.Create(), null);

    public UFOObject Key { get; private set; }
    public UFOObject Value { get; set; }
    public Binding? Next { get; set; }

    private Binding(UFOObject key, UFOObject value, Binding? next)
    {
        Key = key;
        Value = value;
        Next = next;
    }

    public static Binding Create()
    {
        return _EMPTY;
    }

    public static Binding Create(UFOObject key, UFOObject value)
    {
        return new(key, value, _EMPTY);
    }

    public static Binding Create(UFOObject key, UFOObject value, Binding next)
    {
        return new(key, value, next);
    }

    public static Binding Create(Parser.List elems)
    {
        return Create((UFOObject)elems[0], (UFOObject)elems[1]);
    }

    public IEnumerable<Binding> EachElem()
    {
        Binding binding = this;
        while (!binding.IsEmpty())
        {
            yield return binding;
            binding = binding.Next!;
        }
        yield break;
    }

    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        if (ReferenceEquals(this, _EMPTY))
        {
            return this;
        }
        return new Binding(Key.Eval(etor), Value.Eval(etor), (Binding)Next!.Eval(etor));
    }

    public bool IsEmpty()
    {
        return ReferenceEquals(this, _EMPTY);
    }

    public Binding Locate(UFOObject key)
    {
        foreach (Binding binding in EachElem())
        {
            if (key.Equals(binding.Key))
            {
                return binding;
            }
        }
        return _EMPTY;
    }

    public override void ShowOn(TextWriter writer)
    {
        Key.ShowOn(writer);
        writer.Write('=');
        Value.ShowOn(writer);
    }

}
