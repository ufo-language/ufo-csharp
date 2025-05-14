using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Expr;

public class IfThenTests
{
    [Fact]
    public void IfThen_Eval_TruePath()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        UFO.Types.Literal.Boolean cond = UFO.Types.Literal.Boolean.TRUE;
        Integer conseq = Integer.Create(100);
        Integer alt = Integer.Create(200);
        IfThen ifThen = new(cond, conseq, alt);

        // Act
        UFOObject value = ifThen.Eval(etor);

        // Assert
        Assert.Same(conseq, value);
    }

    [Fact]
    public void IfThen_Eval_FalsePath()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        UFO.Types.Literal.Boolean cond = UFO.Types.Literal.Boolean.FALSE;
        Integer conseq = Integer.Create(100);
        Integer alt = Integer.Create(200);
        IfThen ifThen = new(cond, conseq, alt);

        // Act
        UFOObject value = ifThen.Eval(etor);

        // Assert
        Assert.Same(alt, value);
    }
}
