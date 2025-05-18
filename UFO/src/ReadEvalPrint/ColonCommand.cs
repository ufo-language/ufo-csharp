
namespace UFO.ReadEvalPrint;

public class ColonCommand
{
    public static void Exec(string inputString, REP rep)
    {
        if (inputString.StartsWith(":tok"))
        {
            Lexer.Lexer.PrintTokens(rep.Tokens);
        }
        else if (inputString.StartsWith(":expr"))
        {
            Console.WriteLine(rep.Expr);
        }
        else if (inputString.StartsWith(":val"))
        {
            Console.WriteLine(rep.Value);
        }
        else
        {
            Console.WriteLine($"Unknown colon command '{inputString}'");
        }
    }

}
