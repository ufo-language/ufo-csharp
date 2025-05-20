using System.Reflection;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefineAllPrims
{
    public static void DefPrims(Evaluator.Evaluator etor, Set results)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        DefPrims(etor, assembly, results);
    }

    public static void DefPrims(Evaluator.Evaluator etor, Assembly assembly, Set results)
    {
        try
        {
            foreach (Type type in assembly.GetTypes())
            {
                PrimName? attrib = type.GetCustomAttribute<PrimName>();
                if (attrib != null)
                {
                    if (typeof(Primitive).IsAssignableFrom(type))
                    {
                        string longName = string.Join<string>("_", attrib.NameSegments);
                        try
                        {
                            if (Activator.CreateInstance(type, longName) is Primitive instance)
                            {
                                DefinePrim(instance!, attrib.NameSegments, etor, results);
                            }
                            else
                            {
                                Console.Error.WriteLine($"DefineAllPrims.DefinePrims2 could not instantiate {attrib.NameSegments}:{type}");
                            }
                        }
                        catch (MissingMethodException)
                        {
                            Console.Error.WriteLine($"ERROR: Unable to instantiate primitive {type} with arguments (\"{longName}\")");
                        }
                    }
                }
            }
        }
        catch (Exception exn)
        {
            Console.WriteLine("DefineAllPrims caught exception");
            Console.WriteLine(exn);
        }
    }

    // This uses the primitive's PrimName attribute and defines the primitive
    // in that namespace. For example
    //   [PrimName("array", "length")]
    // defines the primitive 'length' in the namespace 'array'.
    private static void DefinePrim(Primitive prim, string[] nameSegments, Evaluator.Evaluator etor, Set results)
    {
        if (nameSegments == null || nameSegments.Length == 0)
            throw new ArgumentException("Primitive nameSegments must have at least one segment");
        Identifier firstIdent = Identifier.Create(nameSegments[0]);
        results.Add(firstIdent);
        if (nameSegments.Length == 1)
        {
            // Top-level binding
            etor.Bind(firstIdent, prim);
            return;
        }
        // Handle nested namespaces
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
