using UFO.Types;

namespace UFO.Utils;

public class ParserListToUFOList
{
    public static List<UFOObject> Convert(Parser.List elems)
    {
        List<UFOObject> ufoElems = [];
        foreach (object elem in elems)
        {
            ufoElems.Add((UFOObject)elem);
        }
        return ufoElems;
    }
}
