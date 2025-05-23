using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Ufo;

public class ModuleDir : Primitive
{
    public ModuleDir()
    {
        ParamTypes = [
            []
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        return Types.Literal.String.Create(Load.MODULE_DIR);
    }
}
