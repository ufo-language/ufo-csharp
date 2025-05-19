namespace UFO.Prims;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PrimAttrib(params string[] nameSegments) : Attribute
{
    public string[] NameSegments = nameSegments;
}