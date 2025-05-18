using UFO.Types;

namespace UFO.DLL.AWS;

public class AWS
{
    public static UFOObject OnLoad(Evaluator.Evaluator etor)
    {
        return Prims.DefinePrims.DefPrims(etor);
    }
}
