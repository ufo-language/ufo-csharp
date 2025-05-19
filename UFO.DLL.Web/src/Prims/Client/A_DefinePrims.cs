using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.Web.Prims.Client;

public class DefinePrims
{
    public static UFOObject DefPrims(Evaluator.Evaluator etor, HashTable ns)
    {
        string nsName = "client";
        HashTable ns1 = HashTable.Create();
        // S3.A_DefinePrims.DefinePrims(ns, nsName);
        // SQS.A_DefinePrims.DefinePrims(ns, nsName);
        Identifier clientId = Identifier.Create(nsName);
        etor.Bind(clientId, ns);
        return clientId;
    }
}
