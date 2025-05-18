using UFO.Lexer;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.ReadEvalPrint;

public class REP
{
    public bool ShowPrompt = true;
    public bool ShowPrint = true;
    public readonly Evaluator.Evaluator Evaluator = new();
    public List<Token> Tokens { get; private set; } = [];
    public UFOObject Expr { get; private set; } = Nil.NIL;
    public UFOObject Value { get; private set; } = Nil.NIL;
    // public bool EOI { get; private set; } = false;
    private string _promptString = "UFO> ";
    private readonly string _PARSER_START = "Program";

    public REP()
    {}

    private void Prompt()
    {
        Console.Out.Write(_promptString);
    }

    public bool ReadEvalPrint(TextReader inputStream)
    {
        if (ShowPrompt)
        {
            Prompt();
        }
        try
        {
            if (!Read(inputStream))
            {
                return false;
            }
            Value = Eval(Expr);
            if (ShowPrint)
            {
                Print(Value);
            }
        }
        catch (Exception exn)
        {
            Console.Error.WriteLine("REP caught exception:");
            Console.Error.WriteLine(exn.Message);
            if (inputStream != Console.In)
            {
                UFOMain.ExitCode = 1;
                // EOI = true;
            }
        }
        return true;
    }

    public bool Read(TextReader inputStream)
    {
        Expr = Nil.NIL;
        string? inputString = inputStream.ReadLine();
        if (inputString == null)
        {
            // EOI = true;
            return false;
        }
        inputString = inputString.Trim();
        if (inputString == "")
        {
            return false;
        }
        if (inputString[0] == ':')
        {
            ColonCommand.Exec(inputString, this);
            return false;
        }
        Lexer.Lexer lexer = new();
        Tokens = lexer.Tokenize(inputString);
        // Lexer.Lexer.PrintTokens(_lexerTokens);
        Parser.ParserState parserState = new(Parser.UFOGrammar.Parsers, Tokens);
        try
        {
            if (Parser.Parser.Parse(_PARSER_START, parserState))
            {
                Expr = (UFOObject)parserState.Value;
                return true;
            }
            return false;
        }
        catch (Parser.ParseException exn)
        {
            Console.Error.WriteLine($"CLI caught exception\n{exn.ToString()}");
            return false;
        }
    }

    public UFOObject Eval(UFOObject expr)
    {
        return expr.Eval(Evaluator);
    }

    public static void Print(UFOObject value)
    {
        if (value != Nil.NIL)
        {
            value.ShowOn(Console.Out);
            Console.Out.Write(" :: ");
            Console.Out.Write(value.GetType().Name);
            Console.Out.WriteLine();
        }
    }
}
