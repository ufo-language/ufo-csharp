namespace UFO;

class UFOMain
{
    public static int ExitCode;

    static int Main(string[] args)
    {
        int exitCode = 0;
        ReadEvalPrint.REPL repl = new();
        Main.CLI.Result result = UFO.Main.CLI.HandleArgs(args, repl);
        if (result == UFO.Main.CLI.Result.EXIT)
        {
            return exitCode;
        }
        repl.Run();
        return exitCode;
    }
}
