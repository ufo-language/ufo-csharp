using System.Data.Common;
using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Evaluator;

public class Environment
{

    private List<Identifier> Names = new();
    private List<UFOObject> Values = new();

    public Environment()
    {}

    public void Bind(Identifier name, UFOObject value)
    {
        Names.Add(name);
        Values.Add(value);
    }

    public bool Locate_Rel(Identifier name, ref int index)
    {
        for (index=1; index<=Names.Count; index++)
        {
            if (ReferenceEquals(name, Names[^index]))
            {
                return true;
            }
        }
        return false;
    }

    public bool Lookup_Rel(Identifier name, ref UFOObject value)
    {
        int index = -1;
        if (Locate_Rel(name, ref index))
        {
            value = Values[^index];
            return true;
        }
        return false;
    }

    public void Rebind_Rel(int index, UFOObject value)
    {
        int index1 = index + 1;
        Values[index1] = value;
    }

    public override string ToString()
    {
        string s = "[";
        bool firstIter = true;
        for (int n=0; n<Names.Count; n++)
        {
            if (firstIter)
                firstIter = false;
            else
                s += ", ";
            Identifier name = Names[n];
            UFOObject value = Values[n];
            s += $"({n}){name}={value}";
        }
        s += "]";
        return s;
    }

}
