using System.Reflection;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Operator;

public class Load : Primitive
{
    private static string _PLUGIN_DIR = "../UFO.Plugins";

    private static HashSet<string> _ALREADY_LOADED = [];

    public Load() : base("load")
    {
        ParamTypes_SumOfProds([TypeId.SYMBOL]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Queue results = Queue.Create();
        foreach (UFOObject arg in args)
        {
            string dllPrefix = arg.ToDisplayString();
            if (_ALREADY_LOADED.Contains(dllPrefix))
            {
                continue;
            }
            UFOObject result = LoadDll(dllPrefix, etor);
            results.Enq(result);
            _ALREADY_LOADED.Add(dllPrefix);
        }
        return results.AsList();
    }

    private static UFOObject LoadDll(string dllPrefix, Evaluator.Evaluator etor)
    {
        string fileName = _PLUGIN_DIR + "/" + dllPrefix + ".dll";
        string fullPath = Path.GetFullPath(fileName);
        Assembly? assembly = Assembly.LoadFrom(fullPath);
#if false
        List<string> namespaceList =
            [.. assembly
                .GetTypes()
                .Select(t => t.Namespace)
                .Where(ns => ns != null)
                .Where(ns => ns!.EndsWith(dllPrefix))
            ];
        if (namespaceList.Count < 1)
        {
            throw new Exception($"Did not find a primary namespace in the {dllPrefix} plugin. It should end with {dllPrefix}.");
        }
        string namespaceName = namespaceList[0];
        string className = $"{namespaceName}.{dllPrefix}";
#endif
        string className = $"UFO.DLL.{dllPrefix}.{dllPrefix}";
        Type? type = assembly!.GetType(className)
            ?? throw new Exception($"Unable to load plugin '{dllPrefix}' from file {fullPath}");
        MethodInfo? method = type.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Static)
            ?? throw new Exception($"Unable to call OnLoad in {dllPrefix} plugin");
        object? returnValue = method.Invoke(null, [etor]);
        return returnValue == null ? Nil.NIL : (UFOObject)returnValue;
    }
}
