using UFO.Types;

namespace UFO.DLL.AWS;

public class AWS
{
    public static UFOObject OnLoad(Evaluator.Evaluator etor)
    {
        return Prims.A_DefinePrims.DefinePrims(etor);
    }
}
