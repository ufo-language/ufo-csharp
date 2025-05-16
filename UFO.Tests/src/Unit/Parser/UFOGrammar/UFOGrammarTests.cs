using UFO.Lexer;
using UFO.Parser;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Parser;

public class UFOGrammarTests
{
    [Fact]
    public void Grammar_TestIfItWorksAtAll()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Integer", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Integer>(value);
        Integer intValue = (Integer)value;
        Assert.Equal(123, intValue.Value);
    }

    [Fact]
    public void Grammar_Integer()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Integer>(value);
        Integer intValue = (Integer)value;
        Assert.Equal(123, intValue.Value);
    }

    [Fact]
    public void Grammar_Symbol()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("Abc");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Symbol>(value);
        Symbol symbolValue = (Symbol)value;
        Assert.Equal("Abc", symbolValue.Name);
    }

    [Fact]
    public void Grammar_Real()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("123.5");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Real>(value);
        Real realValue = (Real)value;
        Assert.Equal(123.5, realValue.Value);
    }

    [Fact]
    public void Grammar_Boolean_true()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("true");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Literal.Boolean>(value);
        UFO.Types.Literal.Boolean boolValue = (UFO.Types.Literal.Boolean)value;
        Assert.True(boolValue.BoolValue);
    }

    [Fact]
    public void Grammar_Boolean_false()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("false");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Literal.Boolean>(value);
        UFO.Types.Literal.Boolean boolValue = (UFO.Types.Literal.Boolean)value;
        Assert.False(boolValue.BoolValue);
    }

    [Fact]
    public void Grammar_Nil()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("nil");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Nil>(value);
    }

    [Fact]
    public void Grammar_Identifierl()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("abc");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Literal", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Identifier>(value);
    }

    [Fact]
    public void Grammar_Seq_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("()");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Seq_1()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("(123)");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Seq_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("(123; Abc)");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Seq", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<Seq>(value);
    }

    [Fact]
    public void Grammar_Array_0()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(0, array.Count);
    }

    [Fact]
    public void Grammar_Array_1()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{123}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(1, array.Count);
    }

    [Fact]
    public void Grammar_Array_2()
    {
        // Arrange
        UFO.Lexer.Lexer lexer = new("{123, Abc}");
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Array", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Array>(value);
        UFO.Types.Data.Array array = (UFO.Types.Data.Array)value;
        Assert.Equal(2, array.Count);
    }

    [Fact]
    public void Grammar_List_0()
    {
        // Arrange
        string inputString = "[]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("List", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.List>(value);
        UFO.Types.Data.List list = (UFO.Types.Data.List)value;
        Assert.Equal(inputString, list.ToString());
    }

    [Fact]
    public void Grammar_List_1()
    {
        // Arrange
        string inputString = "[123]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("List", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.List>(value);
        UFO.Types.Data.List list = (UFO.Types.Data.List)value;
        Assert.Equal(inputString, list.ToString());
    }

    [Fact]
    public void Grammar_List_2()
    {
        // Arrange
        string inputString = "[123, x]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("List", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.List>(value);
        UFO.Types.Data.List list = (UFO.Types.Data.List)value;
        Assert.Equal(inputString, list.ToString());
    }

    [Fact]
    public void Grammar_ListOfArray()
    {
        // Arrange
        string inputString = "[{123}]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("List", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.List>(value);
        UFO.Types.Data.List list = (UFO.Types.Data.List)value;
        Assert.Equal(inputString, list.ToString());
    }

    [Fact]
    public void Grammar_Queue_0()
    {
        // Arrange
        string inputString = "~[]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Queue", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Queue>(value);
        UFO.Types.Data.Queue q = (UFO.Types.Data.Queue)value;
        Assert.Equal(inputString, q.ToString());
    }

    [Fact]
    public void Grammar_Queue_1()
    {
        // Arrange
        string inputString = "~[123]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Queue", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Queue>(value);
        UFO.Types.Data.Queue q = (UFO.Types.Data.Queue)value;
        Assert.Equal(inputString, q.ToString());
    }
    [Fact]

    public void Grammar_Queue_2()
    {
        // Arrange
        string inputString = "~[123, Abc]";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Queue", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Queue>(value);
        UFO.Types.Data.Queue q = (UFO.Types.Data.Queue)value;
        Assert.Equal(inputString, q.ToString());
    }

    [Fact]
    public void Grammar_Set_0()
    {
        // Arrange
        string inputString = "${}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Set", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Set>(value);
        UFO.Types.Data.Set set = (UFO.Types.Data.Set)value;
        Assert.Equal(inputString, set.ToString());
    }

    [Fact]
    public void Grammar_Set_1()
    {
        // Arrange
        string inputString = "${123}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Set", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Set>(value);
        UFO.Types.Data.Set set = (UFO.Types.Data.Set)value;
        Assert.Equal(inputString, set.ToString());
    }

    [Fact]
    public void Grammar_Set_2()
    {
        // Arrange
        string inputString = "${123, Abc}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Set", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Set>(value);
        UFO.Types.Data.Set set = (UFO.Types.Data.Set)value;
        Assert.Equal(inputString, set.ToString());
    }

    [Fact]
    public void Grammar_Term_0()
    {
        // Arrange
        string inputString = "Abc{}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Term", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Term>(value);
        UFO.Types.Data.Term term = (UFO.Types.Data.Term)value;
        Assert.Equal(inputString, term.ToString());
    }

    [Fact]
    public void Grammar_Term_1()
    {
        // Arrange
        string inputString = "Abc{123}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Term", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Term>(value);
        UFO.Types.Data.Term term = (UFO.Types.Data.Term)value;
        Assert.Equal(inputString, term.ToString());
    }

    [Fact]
    public void Grammar_Term_2()
    {
        // Arrange
        string inputString = "Abc{Def{123}, Ghi{456}}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Term", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Term>(value);
        UFO.Types.Data.Term term = (UFO.Types.Data.Term)value;
        Assert.Equal(inputString, term.ToString());
    }

    [Fact]
    public void Grammar_Binding()
    {
        // Arrange
        string inputString = "A=B";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("Binding", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.Binding>(value);
        UFO.Types.Data.Binding binding = (UFO.Types.Data.Binding)value;
        Assert.Equal(inputString, binding.ToString());
    }

    [Fact]
    public void Grammar_HashTable_0()
    {
        // Arrange
        string inputString = "#{}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = UFO.Parser.Parser.Parse("HashTable", parserState);

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.HashTable.ProtoHash>(value);
        UFO.Types.Data.HashTable.ProtoHash protoHash = (UFO.Types.Data.HashTable.ProtoHash)value;
        Assert.Equal(inputString, protoHash.ToString());
    }

    [Fact]
    public void Grammar_HashTable_1()
    {
        // Arrange
        string inputString = "#{A=10}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = false;
        try
        {
            success = UFO.Parser.Parser.Parse("HashTable", parserState);
        }
        catch (ParseException exn)
        {
            Console.WriteLine($"Grammar_HashTable_1 caught exception\n{exn.ToString()}");
            return;
        }

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.HashTable.ProtoHash>(value);
        UFO.Types.Data.HashTable.ProtoHash protoHash = (UFO.Types.Data.HashTable.ProtoHash)value;
        Assert.Equal(inputString, protoHash.ToString());
    }

    [Fact]
    public void Grammar_HashTable_2()
    {
        // Arrange
        string inputString = "#{A=10, B=20}";
        UFO.Lexer.Lexer lexer = new(inputString);
        List<Token> tokens = lexer.Tokenize();
        ParserState parserState = new(UFOGrammar.Parsers, tokens);

        // Act
        bool success = false;
        try
        {
            success = UFO.Parser.Parser.Parse("HashTable", parserState);
        }
        catch (ParseException exn)
        {
            Console.WriteLine($"Grammar_HashTable_1 caught exception\n{exn.ToString()}");
            Assert.Fail();
        }

        // Assert
        Assert.True(success);
        object value = parserState.Value;
        Assert.IsType<UFO.Types.Data.HashTable.ProtoHash>(value);
        UFO.Types.Data.HashTable.ProtoHash protoHash = (UFO.Types.Data.HashTable.ProtoHash)value;
        Assert.Equal(inputString, protoHash.ToString());
    }

}
