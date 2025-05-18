namespace UFO.ReadEvalPrint;

public class REPL
{
    public bool ShowPrompt
    {
        set => _rep.ShowPrompt = value;
    }

    public bool ShowPrint
    {
        set => _rep.ShowPrint = value;
    }

    private readonly REP _rep = new();

    public Evaluator.Evaluator Evaluator => _rep.Evaluator;

    public void Run(TextReader inputStream)
    {
        while (_rep.ReadEvalPrint(inputStream)) ;
        if (inputStream == Console.In)
        {
            Console.WriteLine();
        }
    }

}