using UFO.Types;

namespace UFO.DLL.Web;

public class Web
{
    public static UFOObject OnLoad(Evaluator.Evaluator etor)
    {
        return Prims.DefinePrims.DefPrims(etor);
    }
}
