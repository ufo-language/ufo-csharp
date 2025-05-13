using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UFO.Types;

public abstract class UFOObject
{
    public bool Equals([NotNull] UFOObject other)
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

    public virtual bool EqualsAux([NotNull] UFOObject other)
    {
        return false;
    }

    public abstract void Eval([NotNull] Evaluator.Evaluator etor);

    public override abstract int GetHashCode();

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
