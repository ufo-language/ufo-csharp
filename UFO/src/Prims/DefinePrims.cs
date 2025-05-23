using UFO.Prims.Ufo;
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Prims;

public class DefineAllPrims
{

    public static void DefPrims(Evaluator.Evaluator etor, Set loadedNamespaces)
    {
        DefinePrim(new Array.ExcludeIf(), etor, loadedNamespaces);
        DefinePrim(new Array.ForEach(), etor, loadedNamespaces);
        DefinePrim(new Array.IncludeIf(), etor, loadedNamespaces);
        DefinePrim(new Array.Length(), etor, loadedNamespaces);
        DefinePrim(new Array.Map(), etor, loadedNamespaces);

        DefinePrim(new File.ReadAll(), etor, loadedNamespaces);

        DefinePrim(new Hash.ExcludeIf(), etor, loadedNamespaces);
        DefinePrim(new Hash.ForEach(), etor, loadedNamespaces);
        DefinePrim(new Hash.IncludeIf(), etor, loadedNamespaces);
        DefinePrim(new Hash.Keys(), etor, loadedNamespaces);
        DefinePrim(new Hash.Map(), etor, loadedNamespaces);
        DefinePrim(new Hash.Values(), etor, loadedNamespaces);

        DefinePrim(new Io.Disp(), etor, loadedNamespaces);
        DefinePrim(new Io.Show(), etor, loadedNamespaces);

        DefinePrim(new Json.Parse(), etor, loadedNamespaces);
        DefinePrim(new Json.Stringify(), etor, loadedNamespaces);

        DefinePrim(new List.ExcludeIf(), etor, loadedNamespaces);
        DefinePrim(new List.ForEach(), etor, loadedNamespaces);
        DefinePrim(new List.IncludeIf(), etor, loadedNamespaces);
        DefinePrim(new List.Map(), etor, loadedNamespaces);

        DefinePrim(new Ns.Global(), etor, loadedNamespaces);

        DefinePrim(new Load(), etor, loadedNamespaces);

        DefinePrim(new Os.Cd(), etor, loadedNamespaces);
        DefinePrim(new Os.Cwd(), etor, loadedNamespaces);

        DefinePrim(new Queue.Deq(), etor, loadedNamespaces);
        DefinePrim(new Queue.Enq(), etor, loadedNamespaces);
        DefinePrim(new Queue.Length(), etor, loadedNamespaces);

        DefinePrim(new Term.Attrib(), etor, loadedNamespaces);
        DefinePrim(new Term.SetAttrib(), etor, loadedNamespaces);

        DefinePrim(new Ufo.ModuleDir(), etor, loadedNamespaces);
        DefinePrim(new Ufo.PrimName(), etor, loadedNamespaces);
    }

    // This uses the primitive's Name property and defines the primitive
    // in that namespace. For example
    //   "array_length"
    // defines the primitive 'length' in the namespace 'array'. All nested
    // namespaces are created dynamically as needed.
    private static void DefinePrim(Primitive prim, Evaluator.Evaluator etor, Set loadedNamespaces)
    {
        string[] nameSegments = PrimName.NameOf(prim);
        if (nameSegments == null || nameSegments.Length == 0)
            throw new ArgumentException("Primitive nameSegments must have at least one segment");
        Identifier firstIdent = Identifier.Create(nameSegments[0]);
        loadedNamespaces.Add(firstIdent);
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

#if false
    public static void DefPrims(Evaluator.Evaluator etor, Set loadedNamespaces)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        DefPrims(etor, assembly, loadedNamespaces);
    }

    public static void DefPrims(Evaluator.Evaluator etor, Assembly assembly, Set loadedNamespaces)
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
                                DefinePrim(instance!, attrib.NameSegments, etor, loadedNamespaces);
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
    private static void DefinePrim(Primitive prim, string[] nameSegments, Evaluator.Evaluator etor, Set loadedNamespaces)
    {
        if (nameSegments == null || nameSegments.Length == 0)
            throw new ArgumentException("Primitive nameSegments must have at least one segment");
        Identifier firstIdent = Identifier.Create(nameSegments[0]);
        loadedNamespaces.Add(firstIdent);
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
#endif
}
