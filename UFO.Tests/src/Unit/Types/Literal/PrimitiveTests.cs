using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class PrimitiveTests
{

    class Prim1 : Primitive
    {
        public int NCalls = 0;
        public Pair SavedArgs = Pair.Create();
        public Prim1() : base("Prim1")
        {}
        public override UFOObject Call(Evaluator.Evaluator etor, Pair args)
        {
            NCalls++;
            SavedArgs = args;
            return Nil.Create();
        }
    }

    public static void Apply_CallsPrimitive()
    {
        // Arrange
        Evaluator.Evaluator etor = new();
        Prim1 prim1 = new();
        Integer i100 = Integer.Create(100);
        Pair args = Pair.Create(i100);
        Apply app = Apply.Create(prim1, args);

        // Act
        UFOObject value = app.Eval(etor);

        // Assert
        Assert.Same(Nil.Create(), value);
        Assert.Equal(1, prim1.NCalls);
        Assert.IsType<Pair>(prim1.SavedArgs);
        Assert.False(prim1.SavedArgs.IsEmpty());
        Assert.Equal(i100, prim1.SavedArgs.First);
    }

}
