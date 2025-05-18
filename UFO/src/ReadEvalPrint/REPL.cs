namespace UFO.ReadEvalPrint;

public class REPL
{
    public TextReader InputStream;

    public REPL()
    {
        InputStream = Console.In;
    }

    private readonly REP _rep = new();

    public Evaluator.Evaluator Evaluator => _rep.Evaluator;

    public void Run()
    {
        _rep.EOI = false;
        while (!_rep.EOI)
        {
            _rep.ReadEvalPrint(InputStream);
        }
        if (InputStream == Console.In)
        {
            Console.WriteLine();
        }
    }

    public void RunFile(string fileName)
    {
        try
        {
            using StreamReader reader = new(fileName);
            TextReader savedInputStream = InputStream;
            InputStream = reader;
            Run();
            InputStream = savedInputStream;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"REPL.RunFile file not found: {ex.FileName}");
        }
    }
}