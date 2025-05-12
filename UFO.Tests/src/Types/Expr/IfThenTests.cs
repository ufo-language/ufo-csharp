using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class IfThenTests
{
    [Fact]
    public void IfThen_Eval_SetsUpEvaluatorCorrectly()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Types.Literal.Boolean cond = Types.Literal.Boolean.TRUE;
        Integer conseq = Integer.Create(100);
        Integer alt = Integer.Create(200);
        IfThen ifThen = new(cond, conseq, alt);

        // Act
        ifThen.Eval(etor);

        // Assert
        UFOObject condObj = etor.PopExpr();
        Assert.Same(cond, condObj);
        UFOObject selectContinObj = etor.PopExpr();
        Assert.IsType<IfThen.SelectContin>(selectContinObj);
        UFOObject conseqObj = etor.PopObj();
        Assert.Same(alt, conseqObj);
        UFOObject altObj = etor.PopObj();
        Assert.Same(conseq, altObj);
    }

    [Fact]
    public void IfThen_SelectContin_TruePath_SetsUpEvaluatorCorrectly()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Types.Literal.Boolean cond = Types.Literal.Boolean.TRUE;
        Integer conseq = Integer.Create(100);
        Integer alt = Integer.Create(200);
        IfThen ifThen = new(cond, conseq, alt);

        // Act
        // This sets up the stacks with the SelectContin at the top.
        ifThen.Eval(etor);
        // This evaluates the SelectContin.
        etor.Step();  // evaluate condition
        etor.Step();  // evaluate SelectContin

        // Assert
        UFOObject expr = etor.PopExpr();
        Assert.Same(conseq, expr);
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
    }

    [Fact]
    public void IfThen_SelectContin_FalsePath_SetsUpEvaluatorCorrectly()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Types.Literal.Boolean cond = Types.Literal.Boolean.FALSE;
        Integer conseq = Integer.Create(100);
        Integer alt = Integer.Create(200);
        IfThen ifThen = new(cond, conseq, alt);

        // Act
        // This sets up the stacks with the SelectContin at the top.
        ifThen.Eval(etor);
        // This evaluates the SelectContin.
        etor.Step();  // evaluate condition
        etor.Step();  // evaluate SelectContin

        // Assert
        UFOObject expr = etor.PopExpr();
        Assert.Same(alt, expr);
        Assert.Throws<InvalidOperationException>(() => etor.PopExpr());
        Assert.Throws<InvalidOperationException>(() => etor.PopObj());
    }

}
