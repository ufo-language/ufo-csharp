using System.Text;
using UFO.Types;

namespace UFO.Utils;

public class ToString
{

    public static void ToStringWith(StringBuilder sb, IEnumerable<UFOObject> elems, string open, string sep, string close)
    {
        bool firstIter = true;
        sb.Append(open);
        foreach (UFOObject elem in elems)
        {
            if (firstIter) firstIter = false;
            else sb.Append(sep);
            elem.ToString(sb);
        }
        sb.Append(close);
    }

}
