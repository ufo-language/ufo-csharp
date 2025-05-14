using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Expr;

public class IdentifeirTests
{
    [Fact]
    public void Eval_UnboundIdentifierThrowsException()
    {
        // Arrange
       UFO.Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");

        // Act

        // Assert
        Assert.Throws<Identifier.UnboundIdentifierException>(() => id_x.Eval(etor));
    }

    [Fact]
    public void Eval_BoundIdentifierEvaluatesToValue()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);
        etor.Bind(id_x, i100);

        // Act
        UFOObject value = id_x.Eval(etor);

        // Assert
        Assert.Same(i100, value);
    }

}
