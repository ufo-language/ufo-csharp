using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class FunctionTests
{
    [Fact]
    public void Eval_ReturnsClosure()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");
        List parameters = List.Create(id_x);
        Function fun = Function.Create(parameters, id_x);

        // Act
        UFOObject value = fun.Eval(etor);

        // Assert
        Assert.IsType<Closure>(value);
    }

}
