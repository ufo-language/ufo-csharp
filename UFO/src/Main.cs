using System;
using UFO.Main;

namespace UFO;

class UFOMain
{

    public static int ExitCode;

    static int Main(string[] args)
    {
        // Don't do this, it's ugly. The path is
        // ~/workspace/ufo/csharp/UFO/bin/Debug/net9.0
#if false
        string exeDir = AppContext.BaseDirectory;
        Directory.SetCurrentDirectory(exeDir);
#endif
        int exitCode = 0;
        ReadEvalPrint.REPL repl = new();
        CLI.Result result = CLI.HandleArgs(args, repl);
        if (result == CLI.Result.CONTINUE)
        {
            repl.Run(Console.In);
        }
        return exitCode;
    }
}
