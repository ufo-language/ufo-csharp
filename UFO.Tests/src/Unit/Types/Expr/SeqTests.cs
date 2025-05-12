using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class SeqTests
{
    [Fact]
    public void Seq_0()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Seq seq = Seq.Create();

        // Act
        seq.Eval(etor);

        // Assert
        UFOObject expr = etor.PopObj();
        Assert.Same(Nil.Create(), expr);
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
    }

    [Fact]
    public void Seq_1()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Integer i100 = Integer.Create(100);
        Seq seq = Seq.Create(i100);

        // Act
        seq.Eval(etor);

        // Assert
        UFOObject expr = etor.PopExpr();
        Assert.Same(i100, expr);
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
    }

    [Fact]
    public void Seq_2()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Integer i100 = Integer.Create(100);
        Integer i200 = Integer.Create(100);
        Seq seq = Seq.Create(i100, i200);

        // Act
        seq.Eval(etor);

        // Assert
        Assert.Same(i100, etor.PopExpr());
        Assert.IsType<Seq.DropContin>(etor.PopExpr());
        Assert.Same(i200, etor.PopExpr());
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
    }

}
