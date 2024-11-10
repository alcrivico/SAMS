using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class CategoriaTests
{
    // Prueba para verificar que una instancia de Categoria se inicializa con valores predeterminados
    [Fact]
    public void Categoria_ShouldInitializeWithDefaultValues()
    {
        var categoria = new Categoria();

        Assert.Equal(0, categoria.id);
        Assert.Null(categoria.nombre);
        Assert.False(categoria.estado);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Categoria_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 2;
        var expectedNombre = "Bebidas";
        var expectedEstado = true;

        var categoria = new Categoria
        {
            id = expectedId,
            nombre = expectedNombre,
            estado = expectedEstado
        };

        Assert.Equal(expectedId, categoria.id);
        Assert.Equal(expectedNombre, categoria.nombre);
        Assert.True(categoria.estado);
    }

    // Prueba para verificar que la propiedad 'estado' puede ser falsa
    [Fact]
    public void Categoria_Estado_ShouldBeFalse()
    {
        var categoria = new Categoria
        {
            estado = false
        };

        Assert.False(categoria.estado);
    }

    // Prueba para verificar que 'nombre' puede aceptar un string vacío
    [Fact]
    public void Categoria_Nombre_ShouldAllowEmptyString()
    {
        var categoria = new Categoria
        {
            nombre = ""
        };

        Assert.Equal("", categoria.nombre);
    }

    // Prueba para verificar que 'nombre' puede aceptar un valor nulo
    [Fact]
    public void Categoria_Nombre_ShouldAllowNull()
    {
        var categoria = new Categoria
        {
            nombre = null
        };

        Assert.Null(categoria.nombre);
    }
}