namespace UFO.Types.Literal;

public abstract class Literal(TypeId typeId) : UFOObject(typeId)
{
    public override UFOObject Eval(Evaluator.Evaluator etor)
    {
        return this;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
