using System.Text;
using UFO.Evaluator;
using UFO.Types.Data;
using UFO.Types.Literal;

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

    public virtual UFOObject Apply(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        throw new Exception("Object is not applyable");
    }

    public virtual bool BoolValue => true;

    public virtual void DisplayOn(TextWriter writer)
    {
        ShowOn(writer);
    }

    public virtual bool EqualsAux(UFOObject other)
    {
        return false;
    }

    public abstract UFOObject Eval(Evaluator.Evaluator etor);

    public virtual bool Get(UFOObject indexObj, out UFOObject value)
    {
        value = Nil.NIL;
        return false;
    }

    public override abstract int GetHashCode();

    public static bool Match(List<UFOObject> patterns, List<UFOObject> values, Evaluator.Evaluator etor)
    {
        int nPatterns = patterns.Count;
        int nValues = values.Count;
        if (nPatterns != nValues)
        {
            return false;
        }
        Binding savedEnv = etor.Env;
        for (int n=0; n<nPatterns; n++)
        {
            UFOObject pattern = patterns[n];
            UFOObject value = values[n];
            if (!pattern.Match(value, ref etor))
            {
                etor.Env = savedEnv;
                return false;
            }
        }
        return true;
    }

    public virtual bool Match(UFOObject other, ref Evaluator.Evaluator etor)
    {
        return ReferenceEquals(this, other);
    }

    public virtual void ShowOn(TextWriter writer)
    {
        writer.Write(GetType().Name);
        writer.Write("{}");
    }

    public override string ToString()
    {
        StringWriter sw = new();
        ShowOn(sw);
        return sw.ToString();
    }

}
