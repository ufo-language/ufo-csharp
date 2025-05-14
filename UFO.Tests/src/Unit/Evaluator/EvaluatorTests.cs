namespace UFO.Tests.Unit.Evaluator;

public class EvaluatorTests
{

#if false  // This was for the CPS evaluator
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
#endif
}
