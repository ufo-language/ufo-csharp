using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;

namespace UFO.DLL.Web.Prims;

public class DefinePrims
{
    public static UFOObject DefPrims(Evaluator.Evaluator etor)
    {
        string nsName = "web";
        HashTable ns = HashTable.Create();
        // UFO.DLL.Web.Prims.Client.DefinePrims.DefPrims(ns, nsName);
        // Server.A_DefinePrims.DefinePrims(ns, nsName);
        Identifier webId = Identifier.Create(nsName);
        etor.Bind(webId, ns);
        return webId;
    }
}
