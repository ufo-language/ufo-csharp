using System.Text;
using UFO.Evaluator;
using UFO.Types.Data;

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

    public virtual UFOObject Apply(Evaluator.Evaluator etor, List args)
    {
        throw new Exception("Object is not applyable");
    }

    public virtual bool BoolValue()
    {
        return true;
    }

    public virtual void DisplayOn(TextWriter writer)
    {
        ShowOn(writer);
    }

    public virtual bool EqualsAux(UFOObject other)
    {
        return false;
    }

    public abstract UFOObject Eval(Evaluator.Evaluator etor);

    public override abstract int GetHashCode();

    public virtual bool Match(UFOObject other, ref Evaluator.Evaluator etor)
    {
        return ReferenceEquals(this, other);
    }

    public virtual void ShowOn(TextWriter writer)
    {
        writer.Write(GetType().Name);
        writer.Write("{}");
    }

    public override String ToString()
    {
        StringWriter sw = new();
        ShowOn(sw);
        return sw.ToString();
    }

}
