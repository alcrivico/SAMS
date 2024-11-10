using SAMS.UI.Models.Entities;
namespace SAMS.Test.EntitiesTest;

public class PuestoTests
{
    // Prueba para verificar que una instancia de Puesto se inicializa con valores predeterminados
    [Fact]
    public void Puesto_ShouldInitializeWithDefaultValues()
    {
        var puesto = new Puesto();

        Assert.Equal(0, puesto.id);
        Assert.Null(puesto.nombre);
    }

    // Prueba para verificar que se pueden establecer y obtener los valores de las propiedades
    [Fact]
    public void Puesto_ShouldAllowPropertySetAndGet()
    {
        var expectedId = 1;
        var expectedNombre = "Gerente de Ventas";

        var puesto = new Puesto
        {
            id = expectedId,
            nombre = expectedNombre
        };

        Assert.Equal(expectedId, puesto.id);
        Assert.Equal(expectedNombre, puesto.nombre);
    }

    // Prueba para verificar que el nombre del puesto puede ser un string vacío
    [Fact]
    public void Puesto_Nombre_ShouldAllowEmptyString()
    {
        var puesto = new Puesto
        {
            nombre = ""
        };

        Assert.Equal("", puesto.nombre);
    }

    // Prueba para verificar que el nombre del puesto puede contener hasta 50 caracteres
    [Fact]
    public void Puesto_Nombre_ShouldAllowMaxLength()
    {
        var nombreLargo = new string('A', 50);
        var puesto = new Puesto
        {
            nombre = nombreLargo
        };

        Assert.Equal(50, puesto.nombre.Length);
    }
}