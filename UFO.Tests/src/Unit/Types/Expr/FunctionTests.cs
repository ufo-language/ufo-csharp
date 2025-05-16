using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Expr;

public class FunctionTests
{
    [Fact]
    public void Eval_ReturnsClosure()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");
        List<UFOObject> parameters = [id_x];
        Function fun = Function.Create(parameters, id_x);

        // Act
        UFOObject value = fun.Eval(etor);

        // Assert
        Assert.IsType<Closure>(value);
    }

}
