using UFO.Types;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Expr;

public class SeqTests
{
    [Fact]
    public void Seq_0()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        UFO.Types.Expression.Seq seq = UFO.Types.Expression.Seq.Create();

        // Act
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(Nil.Create(), value);
    }

    [Fact]
    public void Seq_1()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Integer i100 = Integer.Create(100);
        UFO.Types.Expression.Seq seq = UFO.Types.Expression.Seq.Create(i100);

        // Act
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(i100, value);
    }

    [Fact]
    public void Seq_2()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Integer i100 = Integer.Create(100);
        Integer i200 = Integer.Create(100);
        UFO.Types.Expression.Seq seq = UFO.Types.Expression.Seq.Create(i100, i200);

        // Act
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(i200, value);
    }

}
