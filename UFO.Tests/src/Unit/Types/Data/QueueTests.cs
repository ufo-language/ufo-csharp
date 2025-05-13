using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Literal;

namespace UFO.Tests;

public class QueueTests
{

    [Fact]
    public void Deq_Empty_ReturnsFalse()
    {
        // Arrange
        Queue q = new();

        // Act
        bool success = q.Deq(out UFOObject elem);

        // Assert
        Assert.False(success);
    }

#if false  // This was for the CPS evaluator
    [Fact]
    public void Eval_EmptyQueue()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Queue q = new();

        // Act
        etor.PushExpr(q);
        etor.Run();
    
        // Assert
        UFOObject value = etor.PopObj();
        Assert.IsType<Queue>(value);
        Queue qValue = (Queue)value;
        Assert.Equal(0, qValue.Count);
    }

    [Fact]
    public void Eval_OneElement()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Queue q = new();
        Integer i100 = Integer.Create(100);
        q.Enq(i100);

        // Act
        etor.PushExpr(q);
        etor.Run();
    
        // Assert
        UFOObject value = etor.PopObj();
        Assert.IsType<Queue>(value);
        Queue qValue = (Queue)value;
        Assert.Equal(1, qValue.Count);
        Assert.True(qValue.Deq(out UFOObject qElem));
        Assert.Same(i100, qElem);
        Assert.Equal(0, qValue.Count);
    }

    [Fact]
    public void Eval_ThreeElements()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Queue q = new();
        Integer i100 = Integer.Create(100);
        Integer i200 = Integer.Create(200);
        Integer i300 = Integer.Create(300);
        q.Enq(i100, i200, i300);

        // Act
        etor.PushExpr(q);
        etor.Run();
    
        // Assert
        UFOObject value = etor.PopObj();
        Assert.IsType<Queue>(value);
        Queue qValue = (Queue)value;

        Assert.Equal(3, qValue.Count);
        Assert.True(qValue.Deq(out UFOObject qElem));
        Assert.Same(i100, qElem);

        Assert.Equal(2, qValue.Count);
        Assert.True(qValue.Deq(out qElem));
        Assert.Same(i200, qElem);

        Assert.Equal(1, qValue.Count);
        Assert.True(qValue.Deq(out qElem));
        Assert.Same(i300, qElem);

        Assert.Equal(0, qValue.Count);
    }
#endif
}
