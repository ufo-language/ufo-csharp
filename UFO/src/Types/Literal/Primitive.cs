using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using UFO.Types.Data;

namespace UFO.Types.Literal;

public abstract class Primitive(string name) : Literal
{

    private static readonly int _HASH_SEED = typeof(Primitive).GetHashCode();

    public readonly string Name = name;

    protected IEnumerable<TypeId>[] ParamTypes = [];

    private enum ParamTypeTypes
    {
        SUM_OF_PRODS,
        PROD_OF_SUMS
    }

    private ParamTypeTypes ParamTypeType;

    public override UFOObject Apply(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        List<UFOObject> argValues = etor.EvalEach(args);
        CheckArgTypes(argValues);
        return Call(etor, argValues);
    }

    public abstract UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args);

    private void CheckArgTypes(List<UFOObject> args)
    {
        switch (ParamTypeType) {
            case ParamTypeTypes.PROD_OF_SUMS:
                CheckArgTypes_ProdOfSums(args);
                break;
            case ParamTypeTypes.SUM_OF_PRODS:
                CheckArgTypes_SumOfProds(args);
                break;
            default:
                throw new Exception("Unhandled case");
        }
    }

    private void CheckArgTypes_ProdOfSums(List<UFOObject> args)
    {
        // TODO
    }

    private void CheckArgTypes_SumOfProds(List<UFOObject> args)
    {
        // TODO
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(_HASH_SEED, Name.GetHashCode());
    }

    public void ParamTypes_ProdOfSums(params IEnumerable<TypeId>[] paramTypes)
    {
        ParamTypes = paramTypes;
        ParamTypeType = ParamTypeTypes.PROD_OF_SUMS;
    }

    public void ParamTypes_SumOfProds(params IEnumerable<TypeId>[] paramTypes)
    {
        ParamTypes = paramTypes;
        ParamTypeType = ParamTypeTypes.SUM_OF_PRODS;
    }

    public override void ShowOn(TextWriter writer)
    {
        writer.Write("Primitive{");
        writer.Write(Name);
        writer.Write('}');
    }

}
