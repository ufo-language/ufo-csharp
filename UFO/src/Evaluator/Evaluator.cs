using Prims;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Evaluator;

public class Evaluator
{
    public Binding Env { get; private set; }

    public Evaluator()
    {
        Env = Binding.Create();
        DefinePrims.DefineAllPrims(this);
    }

    public void Bind(Identifier name, UFOObject value)
    {
        Env = Binding.Create(name, value, Env);
    }

    public bool Lookup(Identifier name, ref UFOObject value)
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
