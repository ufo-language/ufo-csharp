using System.Reflection;

using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Operator;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DLL_Pre_Load : Attribute
{}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DLL_Post_Load : Attribute
{}

[PrimName("load")]
public class Load : Primitive
{
    private static readonly string _PLUGIN_DIR = "../UFO.Plugins";
    private static readonly string _NAMESPACE_PREFIX = "UFO.DLL";
    private static readonly string _PATH_SEP = "/";
    private static readonly string _DLL_SUFFIX = ".dll";

    private static HashSet<string> _ALREADY_LOADED = [];

    public Load(string longName) : base(longName)
    {
        ParamTypes_SumOfProds([TypeId.SYMBOL]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Types.Data.Queue results = Types.Data.Queue.Create();
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

    private static void RunPreLoad()
    {
    }

    private static void RunPostLoad()
    {
    }
}
