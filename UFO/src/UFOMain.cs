using System;

namespace UFO;

class UFOMain
{
    static void Main(string[] args)
    {
        CLI.REPL repl = new();
        repl.Run();
    }
}
