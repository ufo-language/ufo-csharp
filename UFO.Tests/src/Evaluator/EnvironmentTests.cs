using UFO.Types;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests;

public class EnvironmentTests
{
    [Fact]
    public void Locate_ReturnsCorrectIndex()
    {
        // Arrange
        Evaluator.Environment env = new();
        Identifier id_x = Identifier.Create("x");
        Identifier id_y = Identifier.Create("y");
        Identifier id_z = Identifier.Create("z");
        Integer i100 = Integer.Create(100);
        Integer i200 = Integer.Create(200);
        Integer i300 = Integer.Create(300);

        // Act
        env.Bind(id_x, i100);
        env.Bind(id_y, i200);
        env.Bind(id_z, i300);

        // Assert
        int index = -1;
        Assert.True(env.Locate_Rel(id_z, ref index));
        Assert.Equal(1, index);
        Assert.True(env.Locate_Rel(id_y, ref index));
        Assert.Equal(2, index);
        Assert.True(env.Locate_Rel(id_x, ref index));
        Assert.Equal(3, index);
    }

    [Fact]
    public void BindAndLookup_OneIdentifier()
    {
        // Arrange
        Evaluator.Environment env = new();
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);

        // Act
        env.Bind(id_x, i100);

        // Assert
        UFOObject value = default!;
        bool success = env.Lookup_Rel(id_x, ref value);
        Assert.True(success);
        Assert.Same(i100, value);
    }

    [Fact]
    public void BindAndLookup_MultipleIdentifiers()
    {
        // Arrange
        Evaluator.Environment env = new();
        Identifier id_x = Identifier.Create("x");
        Identifier id_y = Identifier.Create("y");
        Identifier id_z = Identifier.Create("z");
        Integer i100 = Integer.Create(100);
        Integer i200 = Integer.Create(200);
        Integer i300 = Integer.Create(300);

        // Act
        env.Bind(id_x, i100);
        env.Bind(id_y, i200);
        env.Bind(id_z, i300);

        // Assert
        UFOObject value = default!;
        bool success = env.Lookup_Rel(id_z, ref value);
        Assert.True(success);
        Assert.Same(i300, value);
        success = env.Lookup_Rel(id_y, ref value);
        Assert.True(success);
        Assert.Same(i200, value);
        success = env.Lookup_Rel(id_x, ref value);
        Assert.True(success);
        Assert.Same(i100, value);
    }

}
