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
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(Nil.Create(), value);
    }

    [Fact]
    public void Seq_1()
    {
        // Arrange
        Evaluator.Evaluator etor = new Evaluator.Evaluator();
        Integer i100 = Integer.Create(100);
        Seq seq = Seq.Create(i100);

        // Act
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(i100, value);
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
        UFOObject value = seq.Eval(etor);

        // Assert
        Assert.Same(i200, value);
    }

}
