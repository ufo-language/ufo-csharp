using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.File;

public class ReadAll : Primitive
{
    public ReadAll()
    {
        ParamTypes = [
            [TypeId.STRING]
        ];
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        try
        {
            Types.Literal.String fileNameString = (Types.Literal.String)args[0];
            string content = System.IO.File.ReadAllText(fileNameString.Value);
            return Types.Literal.String.Create(content);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
