
CREATE DATABASE [SAMS.Data];
GO
USE [SAMS.Data];
GO

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type IN ('U'))
	DROP TABLE [dbo].[__EFMigrationsHistory]
GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
  [MigrationId] nvarchar(150) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [ProductVersion] nvarchar(32) COLLATE Modern_Spanish_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[__EFMigrationsHistory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Caja
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Caja]') AND type IN ('U'))
	DROP TABLE [dbo].[Caja]
GO

CREATE TABLE [dbo].[Caja] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [noCaja] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [horaDeCorte] datetime2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[Caja] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Categoria
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Categoria]') AND type IN ('U'))
	DROP TABLE [dbo].[Categoria]
GO

CREATE TABLE [dbo].[Categoria] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [estado] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[Categoria] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for DetallePedido
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DetallePedido]') AND type IN ('U'))
	DROP TABLE [dbo].[DetallePedido]
GO

CREATE TABLE [dbo].[DetallePedido] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [cantidad] int  NOT NULL,
  [precioCompra] decimal(18,2)  NOT NULL,
  [pedidoId] int  NOT NULL,
  [productoId] int  NOT NULL,
  [fechaCaducidad] datetime2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[DetallePedido] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for DetalleVenta
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DetalleVenta]') AND type IN ('U'))
	DROP TABLE [dbo].[DetalleVenta]
GO

CREATE TABLE [dbo].[DetalleVenta] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [cantidad] int  NOT NULL,
  [precioVenta] decimal(18,2)  NOT NULL,
  [ventaId] int  NOT NULL,
  [productoInventarioId] int  NOT NULL,
  [ganancia] decimal(18,2)  NOT NULL
)
GO

ALTER TABLE [dbo].[DetalleVenta] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Empleado
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleado]') AND type IN ('U'))
	DROP TABLE [dbo].[Empleado]
GO

CREATE TABLE [dbo].[Empleado] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [rfc] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [noEmpleado] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [apellidoPaterno] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [apellidoMaterno] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [correo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [password] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [telefono] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [estado] bit  NOT NULL,
  [puestoId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Empleado] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for EstadoPedido
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[EstadoPedido]') AND type IN ('U'))
	DROP TABLE [dbo].[EstadoPedido]
GO

CREATE TABLE [dbo].[EstadoPedido] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[EstadoPedido] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for EstadoProducto
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[EstadoProducto]') AND type IN ('U'))
	DROP TABLE [dbo].[EstadoProducto]
GO

CREATE TABLE [dbo].[EstadoProducto] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[EstadoProducto] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Merma
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Merma]') AND type IN ('U'))
	DROP TABLE [dbo].[Merma]
GO

CREATE TABLE [dbo].[Merma] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [cantidad] int  NOT NULL,
  [descripcion] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [fechaRegistro] datetime2(7)  NOT NULL,
  [productoInventarioId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Merma] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Monedero
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Monedero]') AND type IN ('U'))
	DROP TABLE [dbo].[Monedero]
GO

CREATE TABLE [dbo].[Monedero] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [codigoDeBarras] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [saldo] decimal(18,2)  NOT NULL,
  [telefono] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [apellidoPaterno] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [apellidoMaterno] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Monedero] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Pedido
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedido]') AND type IN ('U'))
	DROP TABLE [dbo].[Pedido]
GO

CREATE TABLE [dbo].[Pedido] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [noPedido] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [fechaPedido] datetime2(7)  NOT NULL,
  [fechaEntrega] datetime2(7)  NOT NULL,
  [estadoPedidoId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Pedido] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Producto
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Producto]') AND type IN ('U'))
	DROP TABLE [dbo].[Producto]
GO

CREATE TABLE [dbo].[Producto] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [codigo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [descripcion] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [esDevolvible] bit  NOT NULL,
  [esPerecedero] bit  NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [proveedorId] int  NOT NULL,
  [unidadDeMedidaId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Producto] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for ProductoInventario
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductoInventario]') AND type IN ('U'))
	DROP TABLE [dbo].[ProductoInventario]
GO

CREATE TABLE [dbo].[ProductoInventario] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [codigo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [descripcion] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [cantidadBodega] int  NOT NULL,
  [cantidadExhibicion] int  NOT NULL,
  [precioActual] decimal(18,2)  NOT NULL,
  [esPerecedero] bit  NOT NULL,
  [esDevolvible] bit  NOT NULL,
  [fechaCaducidad] datetime2(7) NULL,
  [unidadDeMedidaId] int  NOT NULL,
  [categoriaId] int  NOT NULL,
  [estadoProductoId] int  NOT NULL,
  [promocionId] int NULL
)
GO

ALTER TABLE [dbo].[ProductoInventario] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Promocion
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Promocion]') AND type IN ('U'))
	DROP TABLE [dbo].[Promocion]
GO

CREATE TABLE [dbo].[Promocion] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [porcentajeDescuento] int  NOT NULL,
  [cantMaxima] int  NOT NULL,
  [cantMinima] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Promocion] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for PromocionVigencia
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[PromocionVigencia]') AND type IN ('U'))
	DROP TABLE [dbo].[PromocionVigencia]
GO

CREATE TABLE [dbo].[PromocionVigencia] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [fechaFin] datetime2(7)  NOT NULL,
  [fechaInicio] datetime2(7)  NOT NULL,
  [promocionId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[PromocionVigencia] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Proveedor
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Proveedor]') AND type IN ('U'))
	DROP TABLE [dbo].[Proveedor]
GO

CREATE TABLE [dbo].[Proveedor] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [rfc] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [correo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [telefono] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [estadoProveedor] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[Proveedor] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Puesto
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Puesto]') AND type IN ('U'))
	DROP TABLE [dbo].[Puesto]
GO

CREATE TABLE [dbo].[Puesto] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Puesto] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for UnidadDeMedida
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[UnidadDeMedida]') AND type IN ('U'))
	DROP TABLE [dbo].[UnidadDeMedida]
GO

CREATE TABLE [dbo].[UnidadDeMedida] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[UnidadDeMedida] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Venta
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Venta]') AND type IN ('U'))
	DROP TABLE [dbo].[Venta]
GO

CREATE TABLE [dbo].[Venta] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [noVenta] int  NOT NULL,
  [fechaRegistro] datetime2(7)  NOT NULL,
  [iva] decimal(18,2)  NOT NULL,
  [totalEfectivo] decimal(18,2)  NOT NULL,
  [totalTarjeta] decimal(18,2)  NOT NULL,
  [totalMonedero] decimal(18,2)  NOT NULL,
  [cajaId] int  NOT NULL,
  [monederoId] int  NOT NULL,
  [empleadoId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Venta] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Caja
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Caja]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Caja
-- ----------------------------
ALTER TABLE [dbo].[Caja] ADD CONSTRAINT [PK_Caja] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Categoria
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Categoria]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Categoria
-- ----------------------------
ALTER TABLE [dbo].[Categoria] ADD CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DetallePedido
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DetallePedido]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table DetallePedido
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_DetallePedido_pedidoId]
ON [dbo].[DetallePedido] (
  [pedidoId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_DetallePedido_productoId]
ON [dbo].[DetallePedido] (
  [productoId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table DetallePedido
-- ----------------------------
ALTER TABLE [dbo].[DetallePedido] ADD CONSTRAINT [PK_DetallePedido] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DetalleVenta
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DetalleVenta]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table DetalleVenta
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_DetalleVenta_productoInventarioId]
ON [dbo].[DetalleVenta] (
  [productoInventarioId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_DetalleVenta_ventaId]
ON [dbo].[DetalleVenta] (
  [ventaId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table DetalleVenta
-- ----------------------------
ALTER TABLE [dbo].[DetalleVenta] ADD CONSTRAINT [PK_DetalleVenta] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Empleado
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Empleado]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table Empleado
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Empleado_puestoId]
ON [dbo].[Empleado] (
  [puestoId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Empleado
-- ----------------------------
ALTER TABLE [dbo].[Empleado] ADD CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for EstadoPedido
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[EstadoPedido]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table EstadoPedido
-- ----------------------------
ALTER TABLE [dbo].[EstadoPedido] ADD CONSTRAINT [PK_EstadoPedido] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for EstadoProducto
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[EstadoProducto]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table EstadoProducto
-- ----------------------------
ALTER TABLE [dbo].[EstadoProducto] ADD CONSTRAINT [PK_EstadoProducto] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Merma
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Merma]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table Merma
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Merma_productoInventarioId]
ON [dbo].[Merma] (
  [productoInventarioId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Merma
-- ----------------------------
ALTER TABLE [dbo].[Merma] ADD CONSTRAINT [PK_Merma] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Monedero
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Monedero]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Monedero
-- ----------------------------
ALTER TABLE [dbo].[Monedero] ADD CONSTRAINT [PK_Monedero] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Pedido
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Pedido]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table Pedido
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Pedido_estadoPedidoId]
ON [dbo].[Pedido] (
  [estadoPedidoId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Pedido
-- ----------------------------
ALTER TABLE [dbo].[Pedido] ADD CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Producto
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Producto]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table Producto
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Producto_proveedorId]
ON [dbo].[Producto] (
  [proveedorId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Producto_unidadDeMedidaId]
ON [dbo].[Producto] (
  [unidadDeMedidaId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Producto
-- ----------------------------
ALTER TABLE [dbo].[Producto] ADD CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ProductoInventario
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ProductoInventario]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table ProductoInventario
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_ProductoInventario_categoriaId]
ON [dbo].[ProductoInventario] (
  [categoriaId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductoInventario_estadoProductoId]
ON [dbo].[ProductoInventario] (
  [estadoProductoId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductoInventario_promocionId]
ON [dbo].[ProductoInventario] (
  [promocionId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductoInventario_unidadDeMedidaId]
ON [dbo].[ProductoInventario] (
  [unidadDeMedidaId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table ProductoInventario
-- ----------------------------
ALTER TABLE [dbo].[ProductoInventario] ADD CONSTRAINT [PK_ProductoInventario] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Promocion
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Promocion]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Promocion
-- ----------------------------
ALTER TABLE [dbo].[Promocion] ADD CONSTRAINT [PK_Promocion] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for PromocionVigencia
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[PromocionVigencia]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table PromocionVigencia
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_PromocionVigencia_promocionId]
ON [dbo].[PromocionVigencia] (
  [promocionId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table PromocionVigencia
-- ----------------------------
ALTER TABLE [dbo].[PromocionVigencia] ADD CONSTRAINT [PK_PromocionVigencia] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Proveedor
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Proveedor]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Proveedor
-- ----------------------------
ALTER TABLE [dbo].[Proveedor] ADD CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Puesto
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Puesto]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Puesto
-- ----------------------------
ALTER TABLE [dbo].[Puesto] ADD CONSTRAINT [PK_Puesto] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for UnidadDeMedida
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[UnidadDeMedida]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table UnidadDeMedida
-- ----------------------------
ALTER TABLE [dbo].[UnidadDeMedida] ADD CONSTRAINT [PK_UnidadDeMedida] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Venta
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Venta]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table Venta
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Venta_cajaId]
ON [dbo].[Venta] (
  [cajaId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Venta_empleadoId]
ON [dbo].[Venta] (
  [empleadoId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Venta_monederoId]
ON [dbo].[Venta] (
  [monederoId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Venta
-- ----------------------------
ALTER TABLE [dbo].[Venta] ADD CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table DetallePedido
-- ----------------------------
ALTER TABLE [dbo].[DetallePedido] ADD CONSTRAINT [FK_DetallePedido_Pedido_pedidoId] FOREIGN KEY ([pedidoId]) REFERENCES [dbo].[Pedido] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[DetallePedido] ADD CONSTRAINT [FK_DetallePedido_Producto_productoId] FOREIGN KEY ([productoId]) REFERENCES [dbo].[Producto] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table DetalleVenta
-- ----------------------------
ALTER TABLE [dbo].[DetalleVenta] ADD CONSTRAINT [FK_DetalleVenta_ProductoInventario_productoInventarioId] FOREIGN KEY ([productoInventarioId]) REFERENCES [dbo].[ProductoInventario] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[DetalleVenta] ADD CONSTRAINT [FK_DetalleVenta_Venta_ventaId] FOREIGN KEY ([ventaId]) REFERENCES [dbo].[Venta] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Empleado
-- ----------------------------
ALTER TABLE [dbo].[Empleado] ADD CONSTRAINT [FK_Empleado_Puesto_puestoId] FOREIGN KEY ([puestoId]) REFERENCES [dbo].[Puesto] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Merma
-- ----------------------------
ALTER TABLE [dbo].[Merma] ADD CONSTRAINT [FK_Merma_ProductoInventario_productoInventarioId] FOREIGN KEY ([productoInventarioId]) REFERENCES [dbo].[ProductoInventario] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Pedido
-- ----------------------------
ALTER TABLE [dbo].[Pedido] ADD CONSTRAINT [FK_Pedido_EstadoPedido_estadoPedidoId] FOREIGN KEY ([estadoPedidoId]) REFERENCES [dbo].[EstadoPedido] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Producto
-- ----------------------------
ALTER TABLE [dbo].[Producto] ADD CONSTRAINT [FK_Producto_Proveedor_proveedorId] FOREIGN KEY ([proveedorId]) REFERENCES [dbo].[Proveedor] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Producto] ADD CONSTRAINT [FK_Producto_UnidadDeMedida_unidadDeMedidaId] FOREIGN KEY ([unidadDeMedidaId]) REFERENCES [dbo].[UnidadDeMedida] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table ProductoInventario
-- ----------------------------
ALTER TABLE [dbo].[ProductoInventario] ADD CONSTRAINT [FK_ProductoInventario_Categoria_categoriaId] FOREIGN KEY ([categoriaId]) REFERENCES [dbo].[Categoria] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[ProductoInventario] ADD CONSTRAINT [FK_ProductoInventario_EstadoProducto_estadoProductoId] FOREIGN KEY ([estadoProductoId]) REFERENCES [dbo].[EstadoProducto] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[ProductoInventario] ADD CONSTRAINT [FK_ProductoInventario_Promocion_promocionId] FOREIGN KEY ([promocionId]) REFERENCES [dbo].[Promocion] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[ProductoInventario] ADD CONSTRAINT [FK_ProductoInventario_UnidadDeMedida_unidadDeMedidaId] FOREIGN KEY ([unidadDeMedidaId]) REFERENCES [dbo].[UnidadDeMedida] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table PromocionVigencia
-- ----------------------------
ALTER TABLE [dbo].[PromocionVigencia] ADD CONSTRAINT [FK_PromocionVigencia_Promocion_promocionId] FOREIGN KEY ([promocionId]) REFERENCES [dbo].[Promocion] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Venta
-- ----------------------------
ALTER TABLE [dbo].[Venta] ADD CONSTRAINT [FK_Venta_Caja_cajaId] FOREIGN KEY ([cajaId]) REFERENCES [dbo].[Caja] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Venta] ADD CONSTRAINT [FK_Venta_Empleado_empleadoId] FOREIGN KEY ([empleadoId]) REFERENCES [dbo].[Empleado] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Venta] ADD CONSTRAINT [FK_Venta_Monedero_monederoId] FOREIGN KEY ([monederoId]) REFERENCES [dbo].[Monedero] ([id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

