using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.Prims.AWS.S3;

public class A_DefinePrims
{
    public static void DefinePrims(HashTable ns, string nsName)
    {
        string nsName1 = "s3";
        string primPrefix = $"{nsName}_{nsName1}";
        HashTable ns1 = HashTable.Create();
        DefineAllPrims.DefPrim(new Put(), ns1, nsName);
        ns[Identifier.Create(nsName1)] = ns;
    }
}
