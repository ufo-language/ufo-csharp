using UFO.ReadEvalPrint;

namespace UFO.Main;

public class CLI
{

    public static readonly string AUTOLOAD_FILENAME = ".autoload.ufo";

    public enum Result
    {
        CONTINUE, EXIT
    }

    public static Result HandleArgs(string[] args, REPL repl)
    {
        List<string> filesToRun = [AUTOLOAD_FILENAME];
        foreach (string arg in args)
        {
            if (arg.EndsWith(".ufo"))
            {
                filesToRun.Add(arg);
            }
            else
            {
                Console.WriteLine($"Unknown command line argument '{arg}'");
            }
        }
        RunFiles(filesToRun, repl);
        if (filesToRun.Count > 1)
        {
            return Result.EXIT;
        }
        return Result.CONTINUE;
    }

    public static void RunFiles(List<string> fileNames, REPL repl)
    {
        try
        {
            foreach (string fileName in fileNames)
            {
                repl.RunFile(fileName);
            }
        }
        catch (Exception exn)
        {
            Console.WriteLine($"CLI.RunFile caught exception {exn.Message}");
            throw;
        }
    }
}
