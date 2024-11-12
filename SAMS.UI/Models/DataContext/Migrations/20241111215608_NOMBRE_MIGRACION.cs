using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAMS.UI.Models.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class NOMBRE_MIGRACION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noCaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    horaDeCorte = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPedido", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoProducto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoProducto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Monedero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoDeBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedero", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    porcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    cantMaxima = table.Column<int>(type: "int", nullable: false),
                    cantMinima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rfc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estadoProveedor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puesto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadDeMedida",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadDeMedida", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoPedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pedido_EstadoPedido_estadoPedidoId",
                        column: x => x.estadoPedidoId,
                        principalTable: "EstadoPedido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocionVigencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    promocionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionVigencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_PromocionVigencia_Promocion_promocionId",
                        column: x => x.promocionId,
                        principalTable: "Promocion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rfc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    puestoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.id);
                    table.ForeignKey(
                        name: "FK_Empleado_Puesto_puestoId",
                        column: x => x.puestoId,
                        principalTable: "Puesto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esDevolvible = table.Column<bool>(type: "bit", nullable: false),
                    esPerecedero = table.Column<bool>(type: "bit", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proveedorId = table.Column<int>(type: "int", nullable: false),
                    unidadDeMedidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.id);
                    table.ForeignKey(
                        name: "FK_Producto_Proveedor_proveedorId",
                        column: x => x.proveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_UnidadDeMedida_unidadDeMedidaId",
                        column: x => x.unidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoInventario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidadBodega = table.Column<int>(type: "int", nullable: false),
                    cantidadExhibicion = table.Column<int>(type: "int", nullable: false),
                    precioActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    esPerecedero = table.Column<bool>(type: "bit", nullable: false),
                    esDevolvible = table.Column<bool>(type: "bit", nullable: false),
                    ubicacion = table.Column<bool>(type: "bit", nullable: false),
                    unidadDeMedidaId = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false),
                    estadoProductoId = table.Column<int>(type: "int", nullable: false),
                    promocionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoInventario", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductoInventario_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoInventario_EstadoProducto_estadoProductoId",
                        column: x => x.estadoProductoId,
                        principalTable: "EstadoProducto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoInventario_Promocion_promocionId",
                        column: x => x.promocionId,
                        principalTable: "Promocion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ProductoInventario_UnidadDeMedida_unidadDeMedidaId",
                        column: x => x.unidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noVenta = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cajaId = table.Column<int>(type: "int", nullable: false),
                    monederoId = table.Column<int>(type: "int", nullable: false),
                    empleadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Venta_Caja_cajaId",
                        column: x => x.cajaId,
                        principalTable: "Caja",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Empleado_empleadoId",
                        column: x => x.empleadoId,
                        principalTable: "Empleado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Monedero_monederoId",
                        column: x => x.monederoId,
                        principalTable: "Monedero",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precioCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pedidoId = table.Column<int>(type: "int", nullable: false),
                    productoId = table.Column<int>(type: "int", nullable: false),
                    fechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Pedido_pedidoId",
                        column: x => x.pedidoId,
                        principalTable: "Pedido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Producto_productoId",
                        column: x => x.productoId,
                        principalTable: "Producto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merma",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    productoInventarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merma", x => x.id);
                    table.ForeignKey(
                        name: "FK_Merma_ProductoInventario_productoInventarioId",
                        column: x => x.productoInventarioId,
                        principalTable: "ProductoInventario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVenta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ventaId = table.Column<int>(type: "int", nullable: false),
                    productoInventarioId = table.Column<int>(type: "int", nullable: false),
                    ganancia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVenta", x => x.id);
                    table.ForeignKey(
                        name: "FK_DetalleVenta_ProductoInventario_productoInventarioId",
                        column: x => x.productoInventarioId,
                        principalTable: "ProductoInventario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVenta_Venta_ventaId",
                        column: x => x.ventaId,
                        principalTable: "Venta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_pedidoId",
                table: "DetallePedido",
                column: "pedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_productoId",
                table: "DetallePedido",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_productoInventarioId",
                table: "DetalleVenta",
                column: "productoInventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_ventaId",
                table: "DetalleVenta",
                column: "ventaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_puestoId",
                table: "Empleado",
                column: "puestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Merma_productoInventarioId",
                table: "Merma",
                column: "productoInventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_estadoPedidoId",
                table: "Pedido",
                column: "estadoPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_proveedorId",
                table: "Producto",
                column: "proveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_unidadDeMedidaId",
                table: "Producto",
                column: "unidadDeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_categoriaId",
                table: "ProductoInventario",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_estadoProductoId",
                table: "ProductoInventario",
                column: "estadoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_promocionId",
                table: "ProductoInventario",
                column: "promocionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoInventario_unidadDeMedidaId",
                table: "ProductoInventario",
                column: "unidadDeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionVigencia_promocionId",
                table: "PromocionVigencia",
                column: "promocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_cajaId",
                table: "Venta",
                column: "cajaId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_empleadoId",
                table: "Venta",
                column: "empleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_monederoId",
                table: "Venta",
                column: "monederoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedido");

            migrationBuilder.DropTable(
                name: "DetalleVenta");

            migrationBuilder.DropTable(
                name: "Merma");

            migrationBuilder.DropTable(
                name: "PromocionVigencia");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "ProductoInventario");

            migrationBuilder.DropTable(
                name: "EstadoPedido");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Monedero");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "EstadoProducto");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "UnidadDeMedida");

            migrationBuilder.DropTable(
                name: "Puesto");
        }
    }
}
