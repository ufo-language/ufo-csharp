using UFO.Types;

namespace UFO.Utils;

public class ShowOn
{

    public static void ShowOnWith(TextWriter writer, IEnumerable<UFOObject> elems, string open, string sep, string close)
    {
        bool firstIter = true;
        writer.Write(open);
        foreach (UFOObject elem in elems)
        {
            if (firstIter) firstIter = false;
            else writer.Write(sep);
            elem.ShowOn(writer);
        }
        writer.Write(close);
    }

}
