using System.Reflection;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefineAllPrims
{
    // Helper method to define a primitive in a namespace.
    // But instead of using this, let it use reflection to find the primitives.
    public static void DefPrim_manual(Primitive prim, HashTable ns, string ns_name)
    {
        ns[Identifier.Create(prim.Name)] = prim;
    }

    public static void DefPrim(Primitive prim, Evaluator.Evaluator etor)
    {
        etor.Bind(Identifier.Create(prim.Name), prim);
    }

    // This uses reflection to look for primitives.
    public static void DefPrims(Evaluator.Evaluator etor)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach (Type type in assembly.GetTypes())
        {
            PrimAttrib? attrib = type.GetCustomAttribute<PrimAttrib>();
            if (attrib != null)
            {
                if (typeof(Primitive).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is Primitive instance)
                    {
                        DefinePrim(instance!, attrib.NameSegments, etor);
                    }
                    else
                    {
                        Console.Error.WriteLine($"DefineAllPrims.DefinePrims2 could not instantiate {attrib.NameSegments}:{type}");
                    }
                }
            }
        }
    }

    private static void DefinePrim(Primitive prim, string[] nameSegments, Evaluator.Evaluator etor)
    {
        if (nameSegments == null || nameSegments.Length == 0)
            throw new ArgumentException("Primitive nameSegments must have at least one segment");
        if (nameSegments.Length == 1)
        {
            // Top-level binding
            Identifier ident = Identifier.Create(nameSegments[0]);
            etor.Bind(ident, prim);
            return;
        }
        // Handle nested namespaces
        Identifier firstIdent = Identifier.Create(nameSegments[0]);
        if (!etor.Lookup(firstIdent, out UFOObject? currentObj))
        {
            currentObj = HashTable.Create();
            etor.Bind(firstIdent, currentObj);
        }
        else if (currentObj is not HashTable)
        {
            throw new InvalidOperationException($"Expected a HashTable at segment '{nameSegments[0]}', found {currentObj?.GetType().Name ?? "null"}");
        }
        HashTable current = (HashTable)currentObj;
        for (int i = 1; i < nameSegments.Length - 1; i++)
        {
            Identifier ident = Identifier.Create(nameSegments[i]);
            if (!current.Get(ident, out UFOObject? nextObj))
            {
                nextObj = HashTable.Create();
                current[ident] = nextObj;
            }
            else if (nextObj is not HashTable)
            {
                throw new InvalidOperationException($"Expected a HashTable at segment '{nameSegments[i]}', found {nextObj?.GetType().Name ?? "null"}");
            }
            current = (HashTable)nextObj;
        }
        Identifier finalIdent = Identifier.Create(nameSegments[^1]);
        current[finalIdent] = prim;
    }
}
