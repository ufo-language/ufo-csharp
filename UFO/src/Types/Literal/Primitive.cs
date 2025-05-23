namespace UFO.Types.Literal;

public abstract class Primitive : Literal
{
    public readonly string Name;
    protected IEnumerable<TypeId>[] ParamTypes = [];
    private static readonly int _HASH_SEED = typeof(Primitive).GetHashCode();
    private static readonly string PREFIX = "UFO.Prims.";

    private enum ParamTypeTypes
    {
        SUM_OF_PRODS,
        PROD_OF_SUMS
    }

    private ParamTypeTypes ParamTypeType = ParamTypeTypes.SUM_OF_PRODS;

    public Primitive()
        : base(TypeId.PRIMITIVE)
    {
        Name = string.Join('_', NameSegments());
    }

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
        Console.WriteLine($"CheckArgTypes_ProdOfSums {this} {args}");
    }

    private void CheckArgTypes_SumOfProds(List<UFOObject> args)
    {
        // TODO
#if false
        Console.WriteLine($"CheckArgTypes_SumOfProds {this} {args}");
        foreach (IEnumerable<TypeId> paramRule in ParamTypes)
        {
            Console.WriteLine($"  checking {paramRule}");
        }
#endif
    }

    public override int GetHashCode()
    {
        return Utils.Hash.CombineHash(_HASH_SEED, Name.GetHashCode());
    }

    public string[] NameSegments()
    {
        string? fullName = this.GetType().FullName;
        if (fullName == null)
        {
            throw new UFOException("PrimName", [
                ("Message", Types.Literal.String.Create("Unable to determine name of primitive")),
                ("Primitive", this)
            ]);
        }
        string name = fullName[PREFIX.Length..];
        string[] parts = name.Split('.');
        for (int n = 0; n < parts.Length; n++)
        {
            parts[n] = char.ToLower(parts[n][0]) + parts[n][1..];
        }
        return parts;
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
