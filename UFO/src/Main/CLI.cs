using UFO.ReadEvalPrint;

namespace UFO.Main;

public class CLI
{

    public enum Result
    {
        CONTINUE, EXIT
    }

    public static Result HandleArgs(string[] args, REPL repl)
    {
        foreach (string arg in args)
        {
            if (arg.EndsWith(".ufo"))
            {
                RunFile(arg, repl);
            }
            return Result.EXIT;
        }
        return Result.CONTINUE;
    }

    public static void RunFile(string fileName, REPL repl)
    {
        repl.ShowPrompt = false;
        repl.ShowPrint = false;
        using StreamReader reader = new(fileName);
        repl.Run(reader);
    }

}
