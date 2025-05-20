using System.Text;
using UFO.Types;

namespace UFO.Parser;

public class List : List<object>
{
    public List<UFOObject> ToListOfUFOObjects()
    {
        List<UFOObject> ufoObjects = [];
        foreach (object elem in this)
        {
            ufoObjects.Add((UFOObject)elem);
        }
        return ufoObjects;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Parser.List[");
        bool firstIter = true;
        foreach (object elem in this)
        {
            if (firstIter) firstIter = false;
            else sb.Append(", ");
            sb.Append(elem);
        }
        sb.Append(']');
        return sb.ToString();
    }
}