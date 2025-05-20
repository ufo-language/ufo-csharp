using System.Linq.Expressions;
using UFO.Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Evaluator;

public class Evaluator
{
    public Binding Env { get; set; }

    public Evaluator()
    {
        Env = Binding.Create();
        Set results = Set.Create();
        DefineAllPrims.DefPrims(this, results);
    }

    public void Bind(Identifier name, UFOObject value)
    {
        Env = Binding.Create(name, value, Env);
    }

    public List<UFOObject> EvalEach(List<UFOObject> elems)
    {
        List<UFOObject> elemVals = [];
        foreach (UFOObject elem in elems)
        {
            elemVals.Add(elem.Eval(this));
        }
        return elemVals;
    }

    public bool Lookup(Identifier name, out UFOObject value)
    {
        value = default!;
        Binding binding = Env.Locate(name);
        if (binding.IsEmpty()) {
            return false;
        }
        value = binding.Value;
        return true;
    }

}
