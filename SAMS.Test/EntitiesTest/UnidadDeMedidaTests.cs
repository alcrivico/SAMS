using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class UnidadDeMedidaTests
{
    // Prueba para verificar que una instancia de UnidadDeMedida se inicializa con valores predeterminados
    [Fact]
    public void UnidadDeMedida_ShouldInitializeWithDefaultValues()
    {
        var unidad = new UnidadDeMedida();

        Assert.Equal(0, unidad.id);
        Assert.Null(unidad.nombre);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void UnidadDeMedida_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNombre = "Kilogramos";

        var unidad = new UnidadDeMedida
        {
            id = expectedId,
            nombre = expectedNombre
        };

        Assert.Equal(expectedId, unidad.id);
        Assert.Equal(expectedNombre, unidad.nombre);
    }

    // Prueba para verificar que el nombre de la unidad puede ser un string vacío
    [Fact]
    public void UnidadDeMedida_Nombre_ShouldAllowEmptyString()
    {
        var unidad = new UnidadDeMedida
        {
            nombre = ""
        };

        Assert.Equal("", unidad.nombre);
    }

    // Prueba para verificar que el nombre de la unidad puede contener hasta 30 caracteres
    [Fact]
    public void UnidadDeMedida_Nombre_ShouldAllowMaxLength()
    {
        var nombreLargo = new string('A', 30);
        var unidad = new UnidadDeMedida
        {
            nombre = nombreLargo
        };

        Assert.Equal(30, unidad.nombre.Length);
    }
}