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
        Types.Data.Set results = Types.Data.Set.Create();
        foreach (UFOObject arg in args)
        {
            string dllPrefix = arg.ToDisplayString();
            if (_ALREADY_LOADED.Contains(dllPrefix))
            {
                continue;
            }
            LoadDll(arg, dllPrefix, etor, results);
            _ALREADY_LOADED.Add(dllPrefix);
        }
        return results;
    }

    private static void LoadDll(UFOObject dllSymbol, string dllPrefix, Evaluator.Evaluator etor, Types.Data.Set results)
    {
        string? moduleDir = Environment.GetEnvironmentVariable("UFO_MODULES");
        moduleDir ??= ".";

        string fileName = $"{moduleDir}{_PATH_SEP}{dllPrefix}{_DLL_SUFFIX}";
        string fullPath = Path.GetFullPath(fileName);
        try
        {
            Assembly? assembly = Assembly.LoadFrom(fullPath);
            string className = $"{_NAMESPACE_PREFIX}.{dllPrefix}.{dllPrefix}";
            Type? type = assembly!.GetType(className)
                ?? throw new Exception($"Unable to load plugin '{dllPrefix}' from file {fullPath}");
            MethodInfo? method = type.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, [etor]);
            // Load primitives
            DefineAllPrims.DefPrims(etor, assembly, results);
        }
        catch (FileNotFoundException)
        {
            throw new UFOException("FileNotFound", [
                ("Message", Types.Literal.String.Create("Unable to load UFO module.")),
                ("Module", dllSymbol),
                ("FileName", Types.Literal.String.Create(fullPath))
            ]);
        }
    }
}
