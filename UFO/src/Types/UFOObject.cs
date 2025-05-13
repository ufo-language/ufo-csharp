using System.Text;
using UFO.Evaluator;

namespace UFO.Types;

public abstract class UFOObject
{
    public bool Equals(UFOObject other)
    {
        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return EqualsAux(other);
    }

    public virtual bool BoolValue()
    {
        return true;
    }

    public virtual bool EqualsAux(UFOObject other)
    {
        return false;
    }

    public abstract void Eval(Evaluator.Evaluator etor);

    public override abstract int GetHashCode();

    public virtual bool Match(UFOObject other, ref Evaluator.Environment env)
    {
        return ReferenceEquals(this, other);
    }

    public override string ToString()
    {  
        StringBuilder sb = new();
        ToString(sb);
        return sb.ToString();
    }

    public virtual void ToString(StringBuilder sb)
    {
        sb.Append(GetType().Name).Append("{}");
    }

}
