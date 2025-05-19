using UFO.Prims;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.AWS.S3;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(HashTable ns, string nsName)
    {
        string nsName1 = "s3";
        string primPrefix = $"{nsName}_{nsName1}";
        HashTable ns1 = HashTable.Create();
        DefineAllPrims.DefPrim_manual(new Client(), ns1, nsName);
        DefineAllPrims.DefPrim_manual(new Delete(), ns1, nsName);
        DefineAllPrims.DefPrim_manual(new Get(), ns1, nsName);
        DefineAllPrims.DefPrim_manual(new List(), ns1, nsName);
        DefineAllPrims.DefPrim_manual(new Put(), ns1, nsName);
        ns[Identifier.Create(nsName1)] = ns1;
    }
}
