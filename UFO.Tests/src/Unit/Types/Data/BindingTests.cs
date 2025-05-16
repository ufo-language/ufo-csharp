using UFO.Types.Data;
using UFO.Types.Expression;
using UFO.Types.Literal;

namespace UFO.Tests.Types.Data.Binding;

public class BindingTests
{
    [Fact]
    public void Binding_Locate_empty()
    {
        // Arrange
        UFO.Types.Data.Binding bindings = UFO.Types.Data.Binding.Create();
        Identifier id_x = Identifier.Create("x");

        // Act
        UFO.Types.Data.Binding locatedBinding = bindings.Locate(id_x);

        // Assert
        Assert.Same(UFO.Types.Data.Binding.Create(), locatedBinding);
    }

    [Fact]
    public void Binding_Locate_singleBinding()
    {
        // Arrange
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);
        UFO.Types.Data.Binding bindings = UFO.Types.Data.Binding.Create(id_x, i100);

        // Act
        UFO.Types.Data.Binding locatedBinding = bindings.Locate(id_x);

        // Assert
        Assert.Same(bindings, locatedBinding);
    }

    [Fact]
    public void Binding_Locate_twoBindings_first()
    {
        // Arrange
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);
        Identifier id_y = Identifier.Create("y");
        Integer i200 = Integer.Create(200);
        UFO.Types.Data.Binding bindings = UFO.Types.Data.Binding.Create(id_x, i100,
            UFO.Types.Data.Binding.Create(id_y, i200));

        // Act
        UFO.Types.Data.Binding locatedBinding = bindings.Locate(id_x);

        // Assert
        Assert.Same(bindings, locatedBinding);
    }
    [Fact]
    public void Binding_Locate_twoBindings_second()
    {
        // Arrange
        Identifier id_x = Identifier.Create("x");
        Integer i100 = Integer.Create(100);
        Identifier id_y = Identifier.Create("y");
        Integer i200 = Integer.Create(200);
        UFO.Types.Data.Binding bindings = UFO.Types.Data.Binding.Create(id_x, i100,
            UFO.Types.Data.Binding.Create(id_y, i200));

        // Act
        UFO.Types.Data.Binding locatedBinding = bindings.Locate(id_y);

        // Assert
        Assert.Same(bindings.Next, locatedBinding);
    }
}
