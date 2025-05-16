using UFO.Lexer;
using UFO.Types;
using UFO.Types.Literal;

namespace UFO.CLI;

public class REP
{
    private readonly Evaluator.Evaluator _etor = new();
    // private readonly Lexer.Lexer _lexer = new();
    private List<Token> _lexerTokens = [];
    private UFOObject _expr = Nil.NIL;
    private UFOObject _value = Nil.NIL;
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
            if (!Read(out _expr))
            {
                Console.WriteLine("Read returned false, ReadEvalPrint returning false");
                return false;
            }
            _value = Eval(_expr);
            Print(_value);
        }
        catch (Exception exn)
        {
            Console.Error.WriteLine("REP caught exception:");
            Console.Error.WriteLine(exn.ToString());
        }
        return true;
    }

    public bool Read(out UFOObject expr)
    {
        expr = Nil.NIL;
        string? inputString = Console.In.ReadLine();
        if (inputString == null)
        {
            EOI = true;
            return false;
        }
        inputString = inputString.Trim();
        if (inputString == "") {
            return false;
        }
        Lexer.Lexer lexer = new();
        _lexerTokens = lexer.Tokenize(inputString);
        Lexer.Lexer.PrintTokens(_lexerTokens);
        Parser.ParserState parserState = new(Parser.UFOGrammar.Parsers, _lexerTokens);
        try
        {
            if (Parser.Parser.Parse(_PARSER_START, parserState))
            {
                expr = (UFOObject)parserState.Value;
                return true;
            }
            Console.WriteLine("Parse failure; Read returning false");
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

    public void Print(UFOObject value)
    {
        value.ShowOn(Console.Out);
        Console.Out.Write(" :: ");
        Console.Out.Write(value.GetType().Name);
        Console.Out.WriteLine();
    }

}
