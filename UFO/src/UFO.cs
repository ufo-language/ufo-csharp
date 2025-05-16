using System;

namespace UFO;

class UFO
{
    static void Main(string[] args)
    {
        CLI.REPL repl = new();
        repl.Run();
    }
}
