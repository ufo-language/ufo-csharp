using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Tests;

public class EvaluatorTests
{
    [Fact]
    public void PushObj()
    {
        // Arrange
        var etor = new Evaluator.Evaluator();
        var nil = Nil.Create();

        // Act
        etor.PushObj(nil);
        UFOObject obj = etor.PopObj();

        // Assert
        Assert.Same(nil, obj);
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
    }
}
