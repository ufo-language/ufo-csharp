
using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Expr;

public class ApplyTests
{
    [Fact]
    public void Eval_ReturnsClosure()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");
        List<UFOObject> parameters = [id_x];
        Function fun = Function.Create(parameters, id_x);
        UFOObject closure = fun.Eval(etor);
        Integer i100 = Integer.Create(100);
        List<UFOObject> args = [i100];
        Apply app = Apply.Create(closure, args);

        // Act
        UFOObject value = app.Eval(etor);

        // Assert
        Assert.Same(i100, value);
    }

}
