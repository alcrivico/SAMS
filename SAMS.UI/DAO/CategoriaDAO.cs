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

        public static IEnumerable<CategoriaDTO> ObtenerCategoriasActivas() => _sams.V_CategoriasActivas.ToList();

        public static bool RegistrarCategoria(string nombreCategoria)
        {
            try
            {
                var categoriaExistente = _sams.Categoria
                                             .FirstOrDefault(c => c.nombre.ToLower() == nombreCategoria.ToLower());

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

        public static bool EditarCategoria(string nombre, string nuevoNombre)
        {
            try
            {
                var categoriaExistente = _sams.Categoria
                                               .FirstOrDefault(c => c.nombre.ToLower() == nombre.ToLower());

                if (categoriaExistente == null)
                {
                    Console.WriteLine($"No se encontró la categoría con el nombre '{nombre}'.");
                    return false;
                }

                var conflictoCategoria = _sams.Categoria
                                               .Any(c => c.nombre.ToLower() == nuevoNombre.ToLower() && c.id != categoriaExistente.id);

                if (conflictoCategoria)
                {
                    Console.WriteLine($"El nombre '{nuevoNombre}' ya está en uso.");
                    return false;
                }

                categoriaExistente.nombre = nuevoNombre;

                _sams.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar la categoría: {ex.Message}");
                return false;
            }
        }

        public static void EliminarCategoria(CategoriaDTO categoria)
        {
            var categoriaExistente = _sams.Categoria
                                           .FirstOrDefault(c => c.nombre.ToLower() == categoria.nombre.ToLower());

            if (categoriaExistente != null)
            {
                categoriaExistente.estado = false;
                _sams.SaveChanges();
            }
            else
            {
                throw new ArgumentException("La categoría no existe.");
            }
        }

    }
}
