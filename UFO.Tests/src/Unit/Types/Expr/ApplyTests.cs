
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class ApplyTests
{
    [Fact]
    public void Eval_ReturnsClosure()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");
        Pair parameters = Pair.Create(id_x);
        Function fun = Function.Create(parameters, id_x);
        UFOObject closure = fun.Eval(etor);
        Integer i100 = Integer.Create(100);
        Pair args = Pair.Create(i100);
        Apply app = Apply.Create(closure, args);

        // Act
        UFOObject value = app.Eval(etor);

        // Assert
        Assert.Same(i100, value);
    }

}
