namespace UFO.CLI;

public class REPL
{
    private readonly REP _rep = new();
    public void Run()
    {
        while (!_rep.EOI)
        {
            _rep.ReadEvalPrint();
        }
        Console.WriteLine();
    }

}