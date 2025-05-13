using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Literal;

public abstract class Primitive : Literal
{

    private static readonly int HashSeed = typeof(Primitive).GetHashCode();

    private readonly string Name;

    protected Primitive(string name)
    {
        Name = name;
    }

    public override UFOObject Apply(Evaluator.Evaluator etor, Pair args)
    {
        Pair argValues = (Pair)args.Eval(etor);
        return Call(etor, argValues);
    }

    public abstract UFOObject Call(Evaluator.Evaluator etor, Pair args);

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(HashSeed, Name.GetHashCode());
    }
    
    public override void ToString(StringBuilder sb)
    {
        sb.Append("Primitive{").Append(Name).Append('}');
    }

}
