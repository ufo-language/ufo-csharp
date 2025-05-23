using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Ufo;

public class PrimName : Primitive
{
    public static readonly string PREFIX = "UFO.Prims.";

    public PrimName()
    {
        ParamTypes = [
            [TypeId.PRIMITIVE]
        ];
    }

    public static string[] NameOf(Primitive prim)
    {
        string? fullName = prim.GetType().FullName;
        if (fullName == null)
        {
            throw new UFOException("PrimName", [
                ("Message", Types.Literal.String.Create("Unable to determine name of primitive")),
                ("Primitive", prim)
            ]);
        }
        string name = fullName[PREFIX.Length..];
        string[] parts = name.Split('.');
        for (int n = 0; n < parts.Length; n++)
        {
            parts[n] = char.ToLower(parts[n][0]) + parts[n][1..];
        }
        return parts;
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Primitive prim = (Primitive)args[0];
        string[] nameSegments = NameOf(prim);
        Console.WriteLine($"Name segments = '{string.Join(':', nameSegments)}'");
        return Nil.NIL;
    }
}
