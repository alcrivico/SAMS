using Microsoft.Extensions.DependencyInjection;
using SAMS.UI.DTO;
using SAMS.UI.Models.DataContext;
using SAMS.UI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.UI.DAO
{
    internal class CategoriaDAO
    {
        private static SAMSContext _sams = App.ServiceProvider.GetRequiredService<SAMSContext>();
        public static IEnumerable<CategoriaDTO> ObtenerCategorias() => _sams.V_Categorias.ToList();

        public static bool RegistrarCategoria(string nombreCategoria)
        {
            try
            {
                var categoriaExistente = _sams.Categoria
                                             .FirstOrDefault(c => c.nombre.Equals(nombreCategoria, StringComparison.OrdinalIgnoreCase));

                if (categoriaExistente != null)
                {
                    return false;
                }

                var nuevaCategoria = new Categoria
                {
                    nombre = nombreCategoria,
                    estado = true  
                };

                _sams.Categoria.Add(nuevaCategoria);
                _sams.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar la categoría: {ex.Message}");
                return false;
            }
        }

    }
}
