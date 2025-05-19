using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.Web.Prims.Server;

public class DefinePrims
{
    public static UFOObject DefPrims(Evaluator.Evaluator etor)
    {
        string nsName = "server";
        HashTable ns = HashTable.Create();
        // S3.A_DefinePrims.DefinePrims(ns, nsName);
        // SQS.A_DefinePrims.DefinePrims(ns, nsName);
        Identifier serverId = Identifier.Create(nsName);
        etor.Bind(serverId, ns);
        return serverId;
    }
}
