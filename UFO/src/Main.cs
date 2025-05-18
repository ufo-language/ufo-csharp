using UFO.Main;

namespace UFO;

class UFOMain
{

    public static int ExitCode;

    static int Main(string[] args)
    {
        int exitCode = 0;
        ReadEvalPrint.REPL repl = new();
        CLI.Result result = CLI.HandleArgs(args, repl);
        if (result == CLI.Result.EXIT)
        {
            return exitCode;
        }
        repl.Run();
        return exitCode;
    }
}
