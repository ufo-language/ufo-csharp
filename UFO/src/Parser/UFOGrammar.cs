using UFO.Lexer;
using UFO.Parser.Prims;
using UFO.Utils;
using UFOArray = UFO.Types.Data.Array;
using UFOBool = UFO.Types.Literal.Boolean;
using UFOIdentifier = UFO.Types.Expression.Identifier;
using UFOInt = UFO.Types.Literal.Integer;
using UFOList = UFO.Types.Data.List;
using UFONil = UFO.Types.Literal.Nil;
using UFOQueue = UFO.Types.Data.Queue;
using UFOReal = UFO.Types.Literal.Real;
using UFOSeq = UFO.Types.Expression.Seq;
using UFOString = UFO.Types.Literal.String;
using UFOSymbol = UFO.Types.Literal.Symbol;

namespace UFO.Parser;

public class UFOGrammar
{

    // Parser shortcut functions
    private static Apply Apply(Func<object, object> function, object parser) => new(function, parser);
    private static IfThen IfThen(object returnValue, object parser) => new(returnValue, parser);
    private static ListOf ListOf(object open, object elem, object sep, object close) => new(open, elem, sep, close);
    private static ListOf ListOf(object open, object elem, object sep, object close, object bar) => new(open, elem, sep, close, bar);
    private static OneOf OneOf(params object[] parsers) => new(parsers);
    // private static Returning Returning(object returnValue, object parser) => new(returnValue, parser);
    private static Seq Seq(params object[] parsers) => new(parsers);
    private static Spot Spot(TokenType tokenType) => new(tokenType);

    // Type conversion functions
    private static readonly Func<object, object> MakeArray = tokenObj => UFOArray.Create((List)tokenObj);
    private static readonly Func<object, object> MakeIdentifier = tokenObj => UFOIdentifier.Create(((Token)tokenObj).Lexeme);
    private static readonly Func<object, object> MakeInt = tokenObj => UFOInt.Create(int.Parse(((Token)tokenObj).Lexeme));
    private static readonly Func<object, object> MakeList = tokenObj => UFOList.Create((List)tokenObj);
    private static readonly Func<object, object> MakeQueue = tokenObj => UFOQueue.Create((List)tokenObj);
    private static readonly Func<object, object> MakeReal = tokenObj => UFOReal.Create(double.Parse(((Token)tokenObj).Lexeme));
    private static readonly Func<object, object> MakeSeq = tokenObj => UFOSeq.Create([.. ParserListToUFOList.Convert((List)tokenObj)]);
    private static readonly Func<object, object> MakeString = tokenObj => UFOString.Create(((Token)tokenObj).Lexeme);
    private static readonly Func<object, object> MakeSymbol = tokenObj => UFOSymbol.Create(((Token)tokenObj).Lexeme);


    public static readonly Dictionary<string, IParser> Parsers = new()
    {
        // ["Number"] = Spot(TokenType.Number),
        // ["Identifier"] = Spot(TokenType.Identifier)

        // 'Program'     : seq('Any', '!EOI'),
        // '!EOI'        : require(Lexer.EOI, 'End-of-Input'),
        // '!Any'        : require('Any'),

        ["Any"]          = OneOf(/*'Apply', 'Assign', 'BinOp', 'Function', 'If', 'PrefixOp', 'Quote', 'ScopeRes', 'Subscript',*/ "Data"),
        // 'Apply'       : apply(Apply.from_parser, seq(recursion_barrier, 'Any', 'ArgList')),
        // 'ArgList'     : list_of('(', 'Any', ',', ')'),
        // 'Assign'      : apply(Assign.from_parser, seq('Data', ':=', '!Any')),
        // 'BinOp'       : apply(BinOp.from_parser, seq(recursion_barrier, 'Any', 'Operator', '!Any')),
        // 'Operator'    : apply(Identifier, spot('Operator')),
        // 'Function'    : apply(Function.from_parser, seq(one_of('fun', 'macro'), one_of('Identifier', succeed(None)), sep_by('FunctionRule', '|'))),
        // 'fun'         : returning(False, spot('Reserved', 'fun')),
        // 'macro'       : returning(True, spot('Reserved', 'macro')),
        // 'FunctionRule': seq('ParamList', '=', 'Any'),
        // 'ParamList'   : list_of('(', 'Any', ',', ')'),
        // 'If'          : apply(IfThen.from_parser, seq('if', '!Any', '!then', '!Any', maybe(seq('else', '!Any')))),
        // '!then'       : require('then'),
        // 'PrefixOp'    : apply(PrefixOp.from_parser, seq('Operator', 'Any')),
        // 'Quote'       : apply(Quote, seq('\'', 'Any', '\'')),
        // 'ScopeRes'    : apply(ScopeResolution.from_parser, sep_by('Identifier', ':', 2)),
        // 'Subscript'   : apply(Subscript.from_parser, seq(recursion_barrier, 'Any', '[', 'Any', ']')),

        ["Data"]         = OneOf("Array", /*'HashTable',*/ "List", "Queue", /*'Set', 'Term',*/ "Literal"),
        ["Array"]        = Apply(MakeArray, ListOf("{", "Any", ",", "}")),
        // # 'Binding'     : apply(Binding.from_parser, seq(recursion_barrier, 'Any', '=', 'Any')),
        // 'HashTable'   : apply(HashTable.ProtoHash.from_parser, seq('#', list_of('{', 'Any', ',', '}'))),
        ["List"]         = Apply(MakeList, ListOf("[", "Any", ",", "]", "|")),
        ["Queue"]        = Apply(MakeQueue, Seq("~", ListOf("[", "Any", ",", "]"))),
        // 'Set'         : apply(Set.from_parser, seq("$", list_of('{', 'Any', ',', '}'))),
        // 'Term'        : apply(Term.from_parser, seq(recursion_barrier, 'Any', 'Array')),

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
