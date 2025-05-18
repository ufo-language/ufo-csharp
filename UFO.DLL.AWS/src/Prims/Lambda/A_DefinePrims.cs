using UFO.Prims;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.AWS.Lambda;

// The A_DefinePrims classes have an A_ prefix just for the purposes of sort order.
public class A_DefinePrims
{
    public static void DefinePrims(HashTable ns, string nsName)
    {
        string nsName1 = "lambda";
        string primPrefix = $"{nsName}_{nsName1}";
        HashTable ns1 = HashTable.Create();
        DefineAllPrims.DefPrim(new Invoke(), ns1, nsName);
        ns[Identifier.Create(nsName1)] = ns1;
    }
}
