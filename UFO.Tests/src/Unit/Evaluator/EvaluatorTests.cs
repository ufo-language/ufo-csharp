using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Evaluator;

public class EvaluatorTests
{
    [Fact]
    public void Bind_noBindings()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Identifier id_x = Identifier.Create("x");

        // Act
        UFOObject value = Nil.NIL;
        bool success = etor.Lookup(id_x, out value);

        // Assert
        Assert.False(success);
    }
}
