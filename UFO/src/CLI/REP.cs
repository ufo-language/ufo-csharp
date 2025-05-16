using UFO.Lexer;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.CLI;

public class REP
{
    private readonly Evaluator.Evaluator _etor = new();
    public List<Token> Tokens { get; private set; } = [];
    public UFOObject Expr { get; private set; } = Nil.NIL;
    public UFOObject Value { get; private set; } = Nil.NIL;
    public bool EOI { get; private set; } = false;
    private string _promptString = "UFO> ";
    private readonly string _PARSER_START = "Program";

    public REP()
    {}

    private void Prompt()
    {
        Console.Out.Write(_promptString);
    }

    public bool ReadEvalPrint()
    {
        Prompt();
        try
        {
            if (!Read())
            {
                return false;
            }
            Value = Eval(Expr);
            Print(Value);
        }
        catch (Exception exn)
        {
            Console.Error.WriteLine("REP caught exception:");
            Console.Error.WriteLine(exn.ToString());
        }
        return true;
    }

    public bool Read()
    {
        Expr = Nil.NIL;
        string? inputString = Console.In.ReadLine();
        if (inputString == null)
        {
            EOI = true;
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
        return expr.Eval(_etor);
    }

    public static void Print(UFOObject value)
    {
        value.ShowOn(Console.Out);
        Console.Out.Write(" :: ");
        Console.Out.Write(value.GetType().Name);
        Console.Out.WriteLine();
    }

}
