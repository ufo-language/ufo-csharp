using UFO.Lexer;
using UFO.Parser.Prims;
using UFO.Utils;

using UFOArray = UFO.Types.Data.Array;
using UFOBinding = UFO.Types.Data.Binding;
using UFOBool = UFO.Types.Literal.Boolean;
using UFOHashTable = UFO.Types.Data.HashTable;
using UFOIdentifier = UFO.Types.Expression.Identifier;
using UFOIfThen = UFO.Types.Expression.IfThen;
using UFOInt = UFO.Types.Literal.Integer;
using UFOList = UFO.Types.Data.List;
using UFONil = UFO.Types.Literal.Nil;
using UFOQueue = UFO.Types.Data.Queue;
using UFOReal = UFO.Types.Literal.Real;
using UFOSeq = UFO.Types.Expression.Seq;
using UFOSet = UFO.Types.Data.Set;
using UFOString = UFO.Types.Literal.String;
using UFOSymbol = UFO.Types.Literal.Symbol;
using UFOTerm = UFO.Types.Data.Term;

namespace UFO.Parser;

public class UFOGrammar
{

    // Parser shortcut functions
    private static Apply Apply(Func<object, object> function, object parser) => new(function, parser);
    private static Debug Debug(object parser) => new(parser);
    private static Debug Debug(string message, object parser) => new(message, parser);
    private static Ignore Ignore(object parser) => new(parser);
    private static IfThen IfThen(object returnValue, object parser) => new(returnValue, parser);
    private static ListOf ListOf(object open, object elem, object sep, object close) => new(open, elem, sep, close);
    private static ListOf ListOf(object open, object elem, object sep, object close, object bar) => new(open, elem, sep, close, bar);
    private static Maybe Maybe(object parser) => new(parser);
    private static OneOf OneOf(params object[] parsers) => new(parsers);
    private static RecursionBarrier RecursionBarrier() => new();
    private static Require Require(object parser) => new(parser);
    private static Require Require(object parser, string message) => new(parser, message);
    private static Seq Seq(params object[] parsers) => new(parsers);
    private static Spot Spot(TokenType tokenType) => new(tokenType);

    // Type conversion functions
    private static readonly Func<object, object> MakeArray = tokenObj => UFOArray.Create((List)tokenObj);
    private static readonly Func<object, object> MakeBinding = tokenObj => UFOBinding.Create((List)tokenObj);
    private static readonly Func<object, object> MakeHashTable = tokenObj => UFOHashTable.ProtoHash.Create((List)tokenObj);
    private static readonly Func<object, object> MakeIdentifier = tokenObj => UFOIdentifier.Create(((Token)tokenObj).Lexeme);
    private static readonly Func<object, object> MakeIfThen = tokenObj => UFOIfThen.Create((List)tokenObj);
    private static readonly Func<object, object> MakeInt = tokenObj => UFOInt.Create(int.Parse(((Token)tokenObj).Lexeme));
    private static readonly Func<object, object> MakeList = tokenObj => UFOList.Create((List)tokenObj);
    private static readonly Func<object, object> MakeQueue = tokenObj => UFOQueue.Create((List)tokenObj);
    private static readonly Func<object, object> MakeReal = tokenObj => UFOReal.Create(double.Parse(((Token)tokenObj).Lexeme));
    private static readonly Func<object, object> MakeSeq = tokenObj => UFOSeq.Create([.. ParserListToUFOList.Convert((List)tokenObj)]);
    private static readonly Func<object, object> MakeSet = tokenObj => UFOSet.Create((List)tokenObj);
    private static readonly Func<object, object> MakeString = tokenObj => UFOString.Create(((Token)tokenObj).Lexeme);
    private static readonly Func<object, object> MakeSymbol = tokenObj => UFOSymbol.Create(((Token)tokenObj).Lexeme);
    private static readonly Func<object, object> MakeTerm = tokenObj => UFOTerm.Create((List)tokenObj);


    public static readonly Dictionary<string, IParser> Parsers = new()
    {
        ["Program"]      = Seq("Any", "!EOI"),
        ["!EOI"]         = Require(Ignore(Spot(TokenType.EOI)), "End-of-Input"),
        ["!Any"]         = Require("Any"),

        ["Any"]          = OneOf(/*"Apply", "Assign", "BinOp", "Function",*/ "IfThen", /*"PrefixOp", "Quote", "ScopeRes", "Subscript",*/ "Data"),
        // "Apply"       : apply(Apply.from_parser, seq(recursion_barrier, "Any", "ArgList")),
        // "ArgList"     : list_of("(", "Any", ",", ")"),
        // "Assign"      : apply(Assign.from_parser, seq("Data", ":=", "!Any")),
        // "BinOp"       : apply(BinOp.from_parser, seq(recursion_barrier, "Any", "Operator", "!Any")),
        // "Operator"    : apply(Identifier, spot("Operator")),
        // "Function"    : apply(Function.from_parser, seq(one_of("fun", "macro"), one_of("Identifier", succeed(None)), sep_by("FunctionRule", "|"))),
        // "fun"         : returning(False, spot("Reserved", "fun")),
        // "macro"       : returning(True, spot("Reserved", "macro")),
        // "FunctionRule": seq("ParamList", "=", "Any"),
        // "ParamList"   : list_of("(", "Any", ",", ")"),
        ["IfThen"]       = Apply(MakeIfThen, Seq("if", "!Any", "!then", "!Any", Maybe(Seq("else", "!Any")))),
        ["!then"]        = Require("then"),
        // "PrefixOp"    : apply(PrefixOp.from_parser, seq("Operator", "Any")),
        // "Quote"       : apply(Quote, seq("\"", "Any", "\"")),
        // "ScopeRes"    : apply(ScopeResolution.from_parser, sep_by("Identifier", ":", 2)),
        // "Subscript"   : apply(Subscript.from_parser, seq(recursion_barrier, "Any", "[", "Any", "]")),

        ["Data"]         = OneOf("Array", "Binding", "HashTable", "List", "Queue", "Set", "Term", "Literal"),
        ["Array"]        = Apply(MakeArray, ListOf("{", "Any", ",", "}")),
        ["Binding"]      = Apply(MakeBinding, Seq(RecursionBarrier(), "Any", "=", "Any")),
        ["HashTable"]    = Apply(MakeHashTable, Seq("#", ListOf("{", "Any", ",", "}"))),
        ["List"]         = Apply(MakeList, ListOf("[", "Any", ",", "]", "|")),
        ["Queue"]        = Apply(MakeQueue, Seq("~", ListOf("[", "Any", ",", "]"))),
        ["Set"]          = Apply(MakeSet, Seq("$", ListOf("{", "Any", ",", "}"))),
        ["Term"]         = Apply(MakeTerm, Seq("Symbol", "Array")),

        ["Literal"]      = OneOf("Boolean", "Integer", "Real", "Identifier", "Nil", "Seq", "String", "Symbol"),
        ["Boolean"]      = OneOf(IfThen("true", UFOBool.TRUE), IfThen("false", UFOBool.FALSE)),
        ["Integer"]      = Apply(MakeInt, Spot(TokenType.Integer)),
        ["Real"]         = Apply(MakeReal, Spot(TokenType.Real)),
        ["Nil"]          = IfThen("nil", UFONil.NIL),
        ["String"]       = Apply(MakeString, Spot(TokenType.String)),
        ["Symbol"]       = Apply(MakeSymbol, Spot(TokenType.Symbol)),

        // # these are not literals, but they are parsed as literals:
        ["Identifier"]   = Apply(MakeIdentifier, Spot(TokenType.Word)),
        ["Seq"]          = Apply(MakeSeq, ListOf("(", "Any", ";", ")"))
    };

}
