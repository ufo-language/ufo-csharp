namespace UFO.Tests.Unit.Types.Literal;

public class IntegerTests
{
#if false  // This was for the CPS evaluator
    [Fact]
    public void Eval()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Integer i100 = Integer.Create(100);

        // Act
        i100.Eval(etor);

        // Assert
        UFOObject obj = etor.PopObj();
        Assert.Same(i100, obj);
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
    }
#endif
}
