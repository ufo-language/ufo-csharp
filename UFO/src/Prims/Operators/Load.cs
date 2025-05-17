using System.Reflection;

using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Prims.Operator;

public class Load : Primitive
{
    private static readonly string _PLUGIN_DIR = "../UFO.Plugins";
    private static readonly string _NAMESPACE_PREFIX = "UFO.DLL";
    private static readonly string _PATH_SEP = "/";
    private static readonly string _DLL_SUFFIX = ".dll";

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
        string fileName = $"{_PLUGIN_DIR}{_PATH_SEP}{dllPrefix}{_DLL_SUFFIX}";
        string fullPath = Path.GetFullPath(fileName);
        Assembly? assembly = Assembly.LoadFrom(fullPath);
        string className = $"{_NAMESPACE_PREFIX}.{dllPrefix}.{dllPrefix}";
        Type? type = assembly!.GetType(className)
            ?? throw new Exception($"Unable to load plugin '{dllPrefix}' from file {fullPath}");
        MethodInfo? method = type.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Static)
            ?? throw new Exception($"Unable to call OnLoad in {dllPrefix} plugin");
        object? returnValue = method.Invoke(null, [etor]);
        return returnValue == null ? Nil.NIL : (UFOObject)returnValue;
    }
}
