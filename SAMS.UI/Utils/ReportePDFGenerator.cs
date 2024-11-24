using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Diagnostics;

namespace SAMS.UI.Utils;

public class ReportePDFGenerator
{
    private readonly string _tituloReporte;
    private readonly string _fechas;

    public ReportePDFGenerator(List<ReportePedidoDTO> pedidos, string fechas)
    {
        _tituloReporte = "Reporte de Pedidos";
        _fechas = fechas;
        GenerarPDF(pedidos);
    }
    public ReportePDFGenerator(List<ReporteVentaDTO> ventas, string fechas)
    {
        _tituloReporte = "Reporte de Ventas";
        _fechas = fechas;
        GenerarPDF(ventas);
    }
    public ReportePDFGenerator(List<ReporteProductoInventarioDTO> inventario, string fechas)
    {
        _tituloReporte = "Reporte de Productos en Inventario";
        _fechas = fechas;
        GenerarPDF(inventario);
    }

    private void GenerarPDF<T>(List<T> datos)
    {
        // Crear el documento
        Document document = new Document();
        document.Info.Title = "Sistema de Administración de Supermercado";

        // Crear la sección
        Section section = document.AddSection();

        // Cabecera del reporte
        Paragraph header = section.AddParagraph("Sistema de Administración de Supermercado");
        header.Format.Font.Size = 16;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;
        section.AddParagraph();

        Paragraph subHeader = section.AddParagraph(_tituloReporte);
        subHeader.Format.Font.Size = 14;
        subHeader.Format.Font.Bold = true;
        subHeader.Format.Alignment = ParagraphAlignment.Center;
        section.AddParagraph($"Fecha de impresion {DateTime.Now}");
        section.AddParagraph($"Rango de fechas: {_fechas}");
        section.AddParagraph();

        // Crear tabla
        Table table = section.AddTable();
        table.Borders.Width = 0.75;

        // Configurar columnas y encabezados
        ConfigurarTabla(datos, table);

        // Agregar datos a la tabla
        AgregarDatosTabla(datos, table);

        // Pie de página con número de página
        Paragraph footer = section.Footers.Primary.AddParagraph();
        footer.AddText("Página ");
        footer.AddPageField();
        footer.AddText(" de ");
        footer.AddNumPagesField();
        footer.Format.Alignment = ParagraphAlignment.Center;

        // Renderizar el documento y guardarlo
        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        renderer.Document = document;
        renderer.RenderDocument();
        string fileName = "Reporte.pdf";
        renderer.PdfDocument.Save(fileName);

        Console.WriteLine($"¡Reporte generado! Archivo: {fileName}");

        // Abrir el PDF para impresión
        AbrirPDFParaImpresion(fileName);
    }

    private void ConfigurarTabla<T>(List<T> datos, Table table)
    {
        if (typeof(T) == typeof(ReportePedidoDTO))
        {
            table.AddColumn("4cm");
            table.AddColumn("3cm");
            table.AddColumn("3cm");
            table.AddColumn("4cm");
            table.AddColumn("3cm");

            Row headerRow = table.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Cells[0].AddParagraph("No. Pedido");
            headerRow.Cells[1].AddParagraph("Fecha Pedido");
            headerRow.Cells[2].AddParagraph("Fecha Entrega");
            headerRow.Cells[3].AddParagraph("Proveedor");
            headerRow.Cells[4].AddParagraph("Costo Total");
        }
        else if (typeof(T) == typeof(ReporteVentaDTO))
        {
            table.AddColumn("3cm");
            table.AddColumn("4cm");
            table.AddColumn("3cm");
            table.AddColumn("3cm");
            table.AddColumn("4cm");

            Row headerRow = table.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Cells[0].AddParagraph("No. Venta");
            headerRow.Cells[1].AddParagraph("Fecha");
            headerRow.Cells[2].AddParagraph("Total");
            headerRow.Cells[3].AddParagraph("Caja");
            headerRow.Cells[4].AddParagraph("Cajero");
        }
        else if (typeof(T) == typeof(ReporteProductoInventarioDTO))
        {
            table.AddColumn("5cm");
            table.AddColumn("3cm");
            table.AddColumn("3cm");
            table.AddColumn("3cm");

            Row headerRow = table.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Cells[0].AddParagraph("Producto");
            headerRow.Cells[1].AddParagraph("Bodega");
            headerRow.Cells[2].AddParagraph("Exhibición");
            headerRow.Cells[3].AddParagraph("Precio");
        }
    }

    private void AgregarDatosTabla<T>(List<T> datos, Table table)
    {
        foreach (var item in datos)
        {
            Row row = table.AddRow();
            if (item is ReportePedidoDTO pedido)
            {
                row.Cells[0].AddParagraph(pedido.noPedido);
                row.Cells[1].AddParagraph(pedido.FechaPedidoFormateada);
                row.Cells[2].AddParagraph(pedido.FechaEntregaFormateada);
                row.Cells[3].AddParagraph(pedido.proveedor);
                row.Cells[4].AddParagraph($"${pedido.costoTotalPedido:F2}");
            }
            else if (item is ReporteVentaDTO venta)
            {
                row.Cells[0].AddParagraph(venta.noVenta.ToString());
                row.Cells[1].AddParagraph(venta.FechaRegistroFormateada);
                row.Cells[2].AddParagraph($"${venta.total:F2}");
                row.Cells[3].AddParagraph(venta.noCaja);
                row.Cells[4].AddParagraph(venta.nombre);
            }
            else if (item is ReporteProductoInventarioDTO producto)
            {
                row.Cells[0].AddParagraph(producto.nombre);
                row.Cells[1].AddParagraph(producto.cantidadBodega);
                row.Cells[2].AddParagraph(producto.cantidadExhibicion);
                row.Cells[3].AddParagraph($"${producto.precioActual:F2}");
            }
        }
    }

    private void AbrirPDFParaImpresion(string filePath)
    {
        try
        {
            Process startProcess = new Process();
            startProcess.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", 
                Arguments = $"/C start \"\" \"{filePath}\"", 
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            startProcess.Start();
            startProcess.WaitForExit(10000); 
            startProcess.Close();
        }
        catch (Exception e)
        {
            InformationControl.Show("Error", $"{e.Message}", "Aceptar");
        }
    }

}
