using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class IdentifeirTests
{
    [Fact]
    public void Eval_UnboundIdentifierThrowsException()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Identifier id_x = Identifier.Create("x");

        // Act
        // id_x.Eval(etor);

        // Assert
        Assert.Throws<Identifier.UnboundIdentifierException>(() => id_x.Eval(etor));
    }

    [Fact]
    public void Eval_BoundIdentifierEvaluatesToValue()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);
        etor.Bind(id_x, i100);

        // Act
        id_x.Eval(etor);

        // Assert
        UFOObject value = etor.PopObj();
        Assert.Same(i100, value);
    }

}
