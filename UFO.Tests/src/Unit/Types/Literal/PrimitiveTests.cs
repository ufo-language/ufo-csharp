using UFO.Types;
using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Unit.Types.Literal;

public class PrimitiveTests
{

    class Prim1 : Primitive
    {
        public int NCalls = 0;
        public List<UFOObject> SavedArgs = [];
        public Prim1()
        {}
        public override UFOObject Call(UFO.Evaluator.Evaluator etor, List<UFOObject> args)
        {
            NCalls++;
            SavedArgs = args;
            return Nil.Create();
        }
    }

    public static void Apply_CallsPrimitive()
    {
        // Arrange
        UFO.Evaluator.Evaluator etor = new();
        Prim1 prim1 = new();
        Integer i100 = Integer.Create(100);
        List<UFOObject> args = [i100];
        Apply app = Apply.Create(prim1, args);

        // Act
        UFOObject value = app.Eval(etor);

        // Assert
        Assert.Same(Nil.Create(), value);
        Assert.Equal(1, prim1.NCalls);
        Assert.IsType<List>(prim1.SavedArgs);
        Assert.True(prim1.SavedArgs.Count > 0);
        Assert.Equal(i100, prim1.SavedArgs[0]);
    }

}
