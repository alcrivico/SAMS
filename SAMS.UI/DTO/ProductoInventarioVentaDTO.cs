﻿namespace SAMS.UI.DTO
{
    public class ProductoInventarioVentaDTO
    {

        public string codigo { get; set; }

        public string nombre { get; set; }

        public decimal precioActual { get; set; }

        public int cantidadExhibicion { get; set; }

        public string unidadDeMedida { get; set; }

        public string? promocion { get; set; }

        public int? porcentajeDescuento { get; set; }

        public int? cantidadMinima { get; set; }

        public int? cantidadMaxima { get; set; }

    }

}
