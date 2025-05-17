using System.Reflection;

using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Prims.Operator;

public class Load : Primitive
{
    private static string _PLUGIN_DIR = "../UFO.Plugins";

    public Load() : base("load")
    {
        ParamTypes_SumOfProds([TypeId.SYMBOL]);
    }

    public override UFOObject Call(Evaluator.Evaluator etor, List<UFOObject> args)
    {
        Symbol dllPrefixSym = (Symbol)args[0];
        string dllPrefix = dllPrefixSym.Name;
        LoadDll(dllPrefix, etor);
        // throw new NotImplementedException();
        return dllPrefixSym;
    }

    private static void LoadDll(string dllPrefix, Evaluator.Evaluator etor)
    {
        string fileName = _PLUGIN_DIR + "/" + dllPrefix + ".dll";
        string fullPath = Path.GetFullPath(fileName);
        Console.WriteLine($"Load.LoadDll attempting to load {fullPath}");
        Assembly assembly = Assembly.LoadFrom(fullPath);
        Type? type = assembly.GetType("AWS.AWSPlugin")
            ?? throw new Exception($"Unable to load plugin '{dllPrefix}' from file {fullPath}");
        MethodInfo? method = type.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Static)
            ?? throw new Exception($"Unable to call OnLoad in {dllPrefix} plugin");
        method.Invoke(null, [etor]);  // no instance, no parameters
    }
}
