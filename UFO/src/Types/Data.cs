namespace UFO.Types.Data;

public abstract class Data(TypeId typeId) : UFOObject(typeId)
{
    public abstract override UFOObject Eval(Evaluator.Evaluator etor);

    public override int GetHashCode()
    {
        throw new NotImplementedException("GetHashCode");
    }
}
