namespace UFO.Prims;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PrimName(params string[] nameSegments) : Attribute
{
    public string[] NameSegments = nameSegments;
}