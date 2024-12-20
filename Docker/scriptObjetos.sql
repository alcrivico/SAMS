-- 1. index

-- 2. vistas
CREATE VIEW V_ProductoInventario AS
SELECT
    pi.id,
    pi.nombre,
    CAST(pi.cantidadBodega AS NVARCHAR(50)) + ' ' + COALESCE(um.nombre, '') AS cantidadBodega,
    CAST(pi.cantidadExhibicion AS NVARCHAR(50)) + ' ' + COALESCE(um.nombre, '') AS cantidadExhibicion,
    pi.precioActual
FROM 
    ProductoInventario pi
LEFT JOIN 
    UnidadDeMedida um ON pi.unidadDeMedidaId = um.id;
GO

CREATE VIEW V_Monederos AS
SELECT
    m.codigoDeBarras,
    m.saldo,
    m.telefono,
    CONCAT(m.nombre, ' ', m.apellidoPaterno, ' ', m.apellidoMaterno) AS nombrePropietario
FROM
    Monedero m
GO

CREATE VIEW V_Monedero AS
SELECT
    m.nombre,
    m.apellidoPaterno,
    m.apellidoMaterno,    
    m.telefono,
    m.saldo,
    m.codigoDeBarras
FROM
    Monedero m
GO

CREATE VIEW V_BusquedaMonedero AS
SELECT
    m.codigoDeBarras,
    m.saldo
FROM
    Monedero m
GO

CREATE VIEW V_ProductoInventarioVenta AS
SELECT
    pi.codigo,
    pi.nombre,
    pi.precioActual,
    pi.cantidadExhibicion,
    um.nombre AS unidadDeMedida,
    p.nombre AS promocion,
    p.porcentajeDescuento,
    p.cantMinima,
    p.cantMaxima
FROM
    ProductoInventario pi
LEFT JOIN
    UnidadDeMedida um
    ON
    pi.unidadDeMedidaId = um.id
LEFT JOIN
    Promocion p
    ON
    pi.promocionId = p.id
GO

CREATE VIEW V_Venta AS
SELECT
    v.noVenta,
    v.fechaRegistro,
    v.iva,
    v.totalEfectivo,
    v.totalTarjeta,
    v.totalMonedero,
    v.tieneRedondeo,
    c.noCaja,
    m.codigoDeBarras AS codigoMonedero,
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombreEmpleado
FROM
    Venta v
INNER JOIN
    Caja c
    ON
    v.cajaId = c.id
INNER JOIN
    Empleado e
    ON
    v.empleadoId = e.id
LEFT JOIN
    Monedero m
    ON
    v.monederoId = m.id
GO

CREATE VIEW V_Ventas AS
SELECT
    v.noVenta,
    SUM(v.totalEfectivo + v.totalTarjeta + v.totalMonedero) AS totalVenta,
    v.fechaRegistro,
    c.noCaja,
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombreEmpleado
FROM
    Venta v
INNER JOIN
    Caja c
    ON
    v.cajaId = c.id
INNER JOIN
    Empleado e
    ON
    v.empleadoId = e.id
GROUP BY
    v.noVenta,
    v.fechaRegistro,
    c.noCaja,
    e.nombre,
    e.apellidoPaterno,
    e.apellidoMaterno;
GO

CREATE VIEW V_DetalleVenta AS
SELECT
    pi.codigo,
    pi.nombre AS nombreDetalleVenta,
    dv.precioVenta AS precio,
    dv.cantidad AS cantidad,
    p.nombre AS promocion,
    CAST(p.porcentajeDescuento AS DECIMAL(18, 2)) / 100 AS porcentajeDescuento, -- Conversión y división
    dv.ganancia AS total,
    p.cantMinima AS cantidadMinima,
    p.cantMaxima AS cantidadMaxima,
    v.noVenta
FROM
    DetalleVenta dv
INNER JOIN
    ProductoInventario pi
    ON
    dv.productoInventarioId = pi.id
INNER JOIN
    Venta v
    ON
    dv.ventaId = v.id
LEFT JOIN
    Promocion p
    ON
    pi.promocionId = p.id
GO

CREATE VIEW V_DetalleVentas AS
SELECT
    pi.codigo,
    pi.nombre,
    dv.cantidad,
    dv.precioVenta,
    dv.ganancia,
    v.noVenta,
    v.fechaRegistro,
    c.noCaja,
    p.nombre AS nombrePromocion
FROM
    DetalleVenta dv
INNER JOIN
    ProductoInventario pi
    ON
    dv.productoInventarioId = pi.id
INNER JOIN
    Venta v
    ON
    dv.ventaId = v.id
INNER JOIN
    Caja c
    ON
    v.cajaId = c.id
LEFT JOIN
    Promocion p
    ON
    pi.promocionId = p.id
GO

CREATE VIEW V_VentasCierreCaja AS
SELECT
    v.noVenta,
    v.fechaRegistro,
    dv.cantidad,
    dv.precioVenta,
    dv.ganancia,
    c.noCaja,
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombreEmpleado
FROM
    Venta v
INNER JOIN
    DetalleVenta dv
    ON
    v.id = dv.ventaId
INNER JOIN
    Caja c
    ON
    v.cajaId = c.id
INNER JOIN
    Empleado e
    ON
    v.empleadoId = e.id
GO

CREATE OR ALTER VIEW V_Promocion AS
SELECT
    p.id,
	p.nombre,
    CAST(pi.cantidadBodega + pi.cantidadExhibicion AS NVARCHAR(50)) + ' ' + COALESCE(um.nombre, '') AS cantidad,
    p.porcentajeDescuento,
    p.cantMaxima,
    p.cantMinima,
    pv.fechaInicio,
	pv.fechaFin
FROM
	Promocion p
INNER JOIN
	PromocionVigencia pv
	ON
	p.id = pv.promocionId
INNER JOIN
    ProductoInventario pi
    ON
    p.id = pi.promocionId
LEFT JOIN
    UnidadDeMedida um
    ON
    pi.unidadDeMedidaId = um.id
WHERE
    pv.fechaFin >= GETDATE();
GO


CREATE VIEW V_Proveedores AS
SELECT
    p.nombre,
    p.rfc,
    p.correo,
    p.telefono,
    CASE 
        WHEN p.estadoProveedor = 1 THEN 'Activo'  -- Si es TRUE                        -- Si es FALSE
    END AS estado
FROM
    Proveedor p
    WHERE p.estadoProveedor = 1;
GO

CREATE VIEW V_Producto AS
SELECT
    p.nombre,
    p.codigo,
    p.descripcion,
    pr.rfc
FROM 
    Producto p
INNER JOIN
    Proveedor pr ON p.proveedorId = pr.id
GO

CREATE VIEW V_Empleados AS
SELECT
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombre,
    e.rfc,
    p.nombre AS puesto,
    e.correo
FROM
    Empleado e
INNER JOIN
    Puesto p
    ON
    e.puestoId = p.id
WHERE e.estado = 1
GO

CREATE VIEW V_EmpleadoDetalle AS
SELECT
    e.nombre,
    e.apellidoPaterno,
    e.apellidoMaterno,
    e.rfc,
    e.noempleado,
    e.correo,
    p.nombre AS puesto,
    e.telefono
FROM 
    Empleado e
INNER JOIN
    Puesto p
    ON
    e.puestoId = p.id
GO

CREATE VIEW dbo.V_ReportePedido AS
SELECT 
    p.NoPedido AS noPedido,
    p.FechaPedido AS fechaPedido,
    p.FechaEntrega AS fechaEntrega,
    pv.nombre AS proveedor,
    SUM(dp.cantidad * dp.precioCompra) AS costoTotalPedido
FROM
    Pedido p
INNER JOIN
    DetallePedido dp 
    ON p.id = dp.pedidoId
INNER JOIN
    Producto pr
    ON dp.productoId = pr.id
INNER JOIN
    Proveedor pv
    ON pr.proveedorId = pv.id
GROUP BY 
    p.NoPedido,
    p.FechaPedido,
    p.FechaEntrega,
    pv.nombre;
GO

CREATE VIEW dbo.V_ReporteVenta AS
SELECT 
    v.noVenta,
    v.fechaRegistro,
    SUM(d.cantidad * d.precioVenta) AS total,
    c.noCaja,
    (e.nombre + ' ' + e.apellidoPaterno + ' ' + e.apellidoMaterno) AS nombre
FROM 
    Venta v
INNER JOIN 
    DetalleVenta d ON v.id = d.ventaId
INNER JOIN 
    Empleado e ON e.id = v.empleadoId
INNER JOIN 
    Caja c ON c.id = v.cajaId
GROUP BY 
    v.noVenta, 
    v.fechaRegistro, 
    c.noCaja, 
    e.nombre, 
    e.apellidoPaterno, 
    e.apellidoMaterno
HAVING 
    SUM(d.cantidad * d.precioVenta) > 0;
GO

CREATE VIEW V_ProductoInventarioPromocion AS
SELECT
    pi.id,
    pi.nombre,
    CAST(pi.cantidadBodega + pi.cantidadExhibicion AS NVARCHAR(50)) + ' ' + COALESCE(um.nombre, '') AS cantidad,
    pi.esPerecedero,
    pi.promocionId
FROM
    ProductoInventario pi
LEFT JOIN
    UnidadDeMedida um ON pi.unidadDeMedidaId = um.id;
GO

-- CU-01 iniciar Sesión
CREATE VIEW V_EmpleadoLogin
AS
SELECT 
    E.correo AS Correo,                 -- Correo del empleado
    E.password AS PasswordHash,         -- Contraseña encriptada
    E.noEmpleado AS NumeroEmpleado,     -- Número de empleado
    E.nombre AS NombreEmpleado,         -- Nombre del empleado
    E.apellidoPaterno AS ApellidoPaterno, -- Apellido paterno
    E.apellidoMaterno AS ApellidoMaterno, -- Apellido materno
    P.nombre AS TipoEmpleado            -- Tipo de empleado desde la tabla Puesto
FROM 
    Empleado E
INNER JOIN 
    Puesto P ON E.puestoId = P.id;      -- Relación con la tabla Puesto
GO

-- CU-03 Registrar producto
CREATE VIEW V_PedidosPendientes AS
SELECT DISTINCT
    P.noPedido AS NoPedido,
    P.fechaPedido AS FechaPedido,
    PR.nombre AS NombreProveedor
FROM 
    Pedido P
INNER JOIN 
    DetallePedido DP ON P.id = DP.pedidoId
INNER JOIN 
    Producto PRD ON DP.productoId = PRD.id
INNER JOIN 
    Proveedor PR ON PRD.proveedorId = PR.id
INNER JOIN 
    EstadoPedido EP ON P.estadoPedidoId = EP.id
WHERE 
    EP.nombre = 'Pendiente';
GO


-- CU-03 Registrar producto
CREATE VIEW V_ProductosPorPedido
AS
SELECT 
    PED.noPedido AS NumeroPedido,
    P.codigo AS CodigoProducto,
    P.nombre AS NombreProducto,
    DP.cantidad AS Cantidad,
    DP.precioCompra AS PrecioCompra
FROM 
    DetallePedido DP
INNER JOIN 
    Producto P ON DP.productoId = P.id
INNER JOIN 
    Pedido PED ON DP.pedidoId = PED.id;
GO

CREATE VIEW V_ProductosRegistrados AS
SELECT 
    PI.codigo AS CodigoProducto,                            -- Código del producto
    PI.nombre AS NombreProducto,                            -- Nombre del producto
    CONCAT(
        PI.cantidadBodega + PI.cantidadExhibicion,          -- Total cantidad con unidad
        ' ',
        UM.nombre,                                          -- Unidad de medida
        CASE 
            WHEN PI.cantidadBodega + PI.cantidadExhibicion > 1 THEN 's'  -- Agregar 's' si la cantidad es mayor a 1
            ELSE ''                                           -- No agregar 's' si la cantidad es 1 o menor
        END
    ) AS Cantidad,                                          -- Resultado final con 's' si aplica
    PI.precioActual AS PrecioActual,                        -- Precio actual del producto
    CAT.nombre AS NombreCategoria                          -- Nombre de la categoría
FROM 
    ProductoInventario PI
INNER JOIN 
    UnidadDeMedida UM ON PI.unidadDeMedidaId = UM.id      -- Relación con unidad de medida
INNER JOIN 
    Categoria CAT ON PI.categoriaId = CAT.id              -- Relación con categoría
INNER JOIN 
    EstadoProducto EP ON PI.estadoProductoId = EP.id      -- Relación con estado de producto
WHERE 
    EP.nombre = 'Disponible';                              -- Solo productos con estado "Disponible"
GO

-- CU-04 Ver producto
CREATE VIEW V_DetalleProducto
AS
SELECT
    PI.codigo AS CodigoProducto,                            -- Código del producto
    PI.nombre AS NombreProducto,                           -- Nombre del producto
    PI.descripcion AS Descripcion,                        -- Descripción
    PI.cantidadBodega AS CantidadBodega,                  -- Cantidad en bodega
    PI.cantidadExhibicion AS CantidadExhibicion,          -- Cantidad en exhibición
    PI.precioActual AS PrecioActual,                      -- Precio actual
    PI.fechaCaducidad AS FechaCaducidad,                  -- Fecha de caducidad (de ProductoInventario)
    CAT.nombre AS NombreCategoria,                        -- Nombre de la categoría
    UM.nombre AS NombreUnidadMedida,                      -- Nombre de la unidad de medida
    PI.esPerecedero AS EsPerecedero,                      -- Perecedero (BIT)
    PI.esDevolvible AS EsDevolvible                       -- Devolvible (BIT)
FROM 
    ProductoInventario PI
INNER JOIN 
    UnidadDeMedida UM ON PI.unidadDeMedidaId = UM.id       -- Relación con unidad de medida
INNER JOIN 
    Categoria CAT ON PI.categoriaId = CAT.id              -- Relación con categoría
GO

--CU 26 "Editar Categoria"
CREATE VIEW V_Categorias AS
SELECT 
    nombre
FROM 
    Categoria;
GO

--CU 28 "Registrar Pedido a Proveedor"
CREATE VIEW V_ProductoPorDetalle AS
SELECT 
    p.nombre,        
    pro.rfc,               
    u.nombre AS nombreUnidadMedida     
FROM 
    Producto p
INNER JOIN 
    UnidadDeMedida u ON p.unidadDeMedidaId = u.id
INNER JOIN 
    Proveedor pro ON p.proveedorId = pro.id;
GO

--CU 29 "Consultar Pedido a Proveedor"
CREATE VIEW V_Pedidos 
AS
SELECT DISTINCT 
    p.id AS idPedido, 
    p.noPedido,
    prov.nombre AS nombreProveedor,  
    p.fechaPedido, 
    p.fechaEntrega,
    ep.nombre AS nombreEstado
FROM 
	Pedido p
INNER JOIN 
	EstadoPedido ep ON p.estadoPedidoId = ep.id
INNER JOIN 
	DetallePedido dp ON p.id = dp.pedidoId
INNER JOIN 
	Producto prod ON dp.productoId = prod.id
INNER JOIN 
	Proveedor prov ON prod.proveedorId = prov.id
WHERE 
	ep.nombre IN ('Pendiente', 'Entregado'); 
GO

CREATE VIEW V_DetallesPedido AS
SELECT 
	p.id As idPedido,
	pro.nombre AS nombreProducto,
	um.nombre AS nombreUnidadMedida,
	dp.cantidad,
	dp.precioCompra
FROM DetallePedido dp
JOIN Pedido p ON dp.pedidoId = p.id
JOIN Producto pro ON dp.productoId = pro.id
JOIN EstadoPedido ep ON p.estadoPedidoId = ep.id
JOIN UnidadDeMedida um ON pro.unidadDeMedidaId = um.id;
GO


-- 3. procedimientos almacenados
-- funciones listas


-- funciones escalares

-- procedimientos almacenados

CREATE PROCEDURE SP_CambiarEstadoProductoAgotado
    @codigoProducto NVARCHAR(MAX) -- Código del producto a cambiar de estado
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT 1 FROM ProductoInventario WHERE codigo = @codigoProducto)
        BEGIN
            RAISERROR('El producto no existe en ProductoInventario.', 16, 1);
            RETURN;
        END

        -- Obtener el ID del estado "Agotado"
        DECLARE @idEstadoAgotado INT;
        SELECT @idEstadoAgotado = id 
        FROM EstadoProducto 
        WHERE nombre = 'Agotado';

        -- Validar que el estado "Agotado" existe
        IF @idEstadoAgotado IS NULL
        BEGIN
            RAISERROR('El estado "Agotado" no existe en la tabla EstadoProducto.', 16, 1);
            RETURN;
        END

        -- Actualizar el estado del producto a "Agotado"
        UPDATE ProductoInventario
        SET estadoProductoId = @idEstadoAgotado
        WHERE codigo = @codigoProducto;
    END TRY
    BEGIN CATCH
        -- Manejar errores (si es necesario)
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE SP_CambiarEstadoPedidoCancelado
    @noPedido NVARCHAR(MAX) -- Número del pedido a cambiar de estado
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si el pedido existe
        IF NOT EXISTS (SELECT 1 FROM Pedido WHERE noPedido = @noPedido)
        BEGIN
            RAISERROR('El pedido no existe en la tabla Pedido.', 16, 1);
            RETURN;
        END

        -- Obtener el ID del estado "Cancelado"
        DECLARE @idEstadoCancelado INT;
        SELECT @idEstadoCancelado = id 
        FROM EstadoPedido 
        WHERE nombre = 'Cancelado';

        -- Validar que el estado "Cancelado" existe
        IF @idEstadoCancelado IS NULL
        BEGIN
            RAISERROR('El estado "Cancelado" no existe en la tabla EstadoPedido.', 16, 1);
            RETURN;
        END

        -- Actualizar el estado del pedido a "Cancelado"
        UPDATE Pedido
        SET estadoPedidoId = @idEstadoCancelado
        WHERE noPedido = @noPedido;
    END TRY
    BEGIN CATCH 
        -- Manejar errores
        THROW;
    END CATCH
END;
GO

-- procedimiento transaccional
CREATE PROCEDURE [dbo].T_CrearPromocionConVigencia
(
	@nombre NVARCHAR(100),
	@porcentajeDescuento INT,
	@cantMaxima INT,
	@cantMinima INT,
	@fechaInicio DATE,
	@fechaFin DATE,
	@roductoInventarioId INT
)
AS
BEGIN
	-- Iniciar la transacción
	BEGIN TRANSACTION;

	BEGIN TRY
		-- 1. Insertar en la tabla Promocion
		DECLARE @promocionId INT;

		INSERT INTO 
			Promocion (nombre, porcentajeDescuento, cantMaxima, cantMinima)
		VALUES 
			(@nombre, @porcentajeDescuento, @cantMaxima, @cantMinima);

		-- Obtener el ID de la promoción insertada
		SET @promocionId = SCOPE_IDENTITY();

		-- 2. Insertar en la tabla PromocionVigencia
		INSERT INTO 
			PromocionVigencia (fechaInicio, fechaFin, promocionId)
		VALUES 
			(@fechaInicio, @fechaFin, @promocionId);

		-- 3. Actualizar la tabla ProductoInventario con el idPromocion
		UPDATE 
			ProductoInventario
		SET promocionId = @promocionId
		WHERE 
			id = @roductoInventarioId;

		-- Confirmar la transacción
		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		-- Si ocurre un error, deshacer la transacción
		ROLLBACK TRANSACTION;
		
		-- Mostrar el error
		THROW;
	END CATCH;
END;
GO

CREATE PROCEDURE [dbo].T_EditarPromocion
(
    @promocionId INT,
    @nombre NVARCHAR(100),
    @porcentajeDescuento INT,
    @fechaInicio DATE,
    @fechaFin DATE
)
AS
BEGIN
    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Comparar y actualizar Promocion
        IF EXISTS (SELECT 1 FROM Promocion WHERE id = @promocionId AND (nombre <> @nombre OR porcentajeDescuento <> @porcentajeDescuento))
        BEGIN
            UPDATE 
                Promocion
            SET 
                nombre = @nombre,
                porcentajeDescuento = @porcentajeDescuento
            WHERE 
                id = @promocionId;
        END

        -- 2. Comparar y actualizar PromocionVigencia
        IF EXISTS 
            (SELECT 
                1 
            FROM 
                PromocionVigencia 
            WHERE 
                promocionId = @promocionId 
            AND 
                (fechaInicio <> @fechaInicio OR fechaFin <> @fechaFin))
        BEGIN
            UPDATE 
                PromocionVigencia
            SET 
                fechaInicio = @fechaInicio,
                fechaFin = @fechaFin
            WHERE 
                promocionId = @promocionId;
        END

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY

    BEGIN CATCH
        -- Si ocurre un error, deshacer la transacción
        ROLLBACK TRANSACTION;
        
        -- Mostrar el error
        THROW;
    END CATCH;
END;
GO

CREATE TYPE DetalleVentaType AS TABLE (
    codigo NVARCHAR(MAX),
    cantidad INT,
    precio DECIMAL(18, 2),
    total DECIMAL(18, 2)
);
GO

CREATE PROCEDURE T_RegistrarVenta
    @pagoEfectivo DECIMAL(18, 2) = NULL, -- Acepta valores NULL
    @pagoTarjeta DECIMAL(18, 2) = NULL,  -- Acepta valores NULL
    @pagoMonedero DECIMAL(18, 2) = NULL, -- Acepta valores NULL
    @iva DECIMAL(18, 2),
    @codigoMonedero NVARCHAR(MAX) = NULL, -- Parámetro opcional
    @noEmpleado NVARCHAR(MAX),
    @noCaja NVARCHAR(MAX),
    @tieneRedondeo BIT,
    @detalles DetalleVentaType READONLY -- Tipo de tabla como parámetro
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Asignar valores predeterminados si los parámetros son NULL
        SET @pagoEfectivo = ISNULL(@pagoEfectivo, 0.00);
        SET @pagoTarjeta = ISNULL(@pagoTarjeta, 0.00);
        SET @pagoMonedero = ISNULL(@pagoMonedero, 0.00);

        -- 1. Obtener IDs
        DECLARE @monederoId INT = NULL, @empleadoId INT, @cajaId INT;

        -- Validar y obtener el ID del monedero si se proporciona un código
        IF @codigoMonedero IS NOT NULL AND @pagoMonedero > 0
        BEGIN
            SELECT @monederoId = id
            FROM Monedero
            WHERE codigoDeBarras = @codigoMonedero;

            IF @monederoId IS NULL
            BEGIN
                THROW 50000, 'El código de monedero proporcionado no existe.', 1;
            END;
        END;

        -- Obtener el ID del empleado
        SELECT @empleadoId = id
        FROM Empleado
        WHERE noEmpleado = @noEmpleado;

        IF @empleadoId IS NULL
        BEGIN
            THROW 50000, 'El empleado especificado no existe.', 1;
        END;

        -- Obtener el ID de la caja
        SELECT @cajaId = id
        FROM Caja
        WHERE noCaja = @noCaja;

        IF @cajaId IS NULL
        BEGIN
            THROW 50000, 'La caja especificada no existe.', 1;
        END;

        -- 2. Registrar la Venta
        DECLARE @ventaId INT, @noVenta INT;

        SELECT @noVenta = ISNULL(MAX(noVenta), 0) + 1
        FROM Venta;

        INSERT INTO Venta (
            noVenta,
            fechaRegistro,
            iva,
            totalEfectivo,
            totalTarjeta,
            totalMonedero,
            tieneRedondeo,
            cajaId,
            monederoId,
            empleadoId
        )
        VALUES (
            @noVenta,
            GETDATE(),
            @iva,
            @pagoEfectivo,
            @pagoTarjeta,
            @pagoMonedero,
            @tieneRedondeo,
            @cajaId,
            @monederoId, -- Puede ser NULL
            @empleadoId
        );

        SET @ventaId = SCOPE_IDENTITY();

        -- 3. Registrar Detalles de Venta y Actualizar Inventario
        INSERT INTO DetalleVenta (
			codigo,
            cantidad,
            precioVenta,
            ventaId,
            productoInventarioId,
            ganancia
        )
        SELECT
			dv.codigo,
            dv.cantidad,
            dv.precio,
            @ventaId,
            pi.id,
            dv.total -- Usamos el total proporcionado como ganancia
        FROM @detalles dv
        INNER JOIN ProductoInventario pi
            ON dv.codigo = pi.codigo;

        -- 4. Actualizar el Inventario
        UPDATE pi
        SET pi.cantidadExhibicion = pi.cantidadExhibicion - dv.cantidad
        FROM ProductoInventario pi
        INNER JOIN @detalles dv
            ON dv.codigo = pi.codigo;

        -- Validación de cantidades negativas
        IF EXISTS (
            SELECT 1
            FROM ProductoInventario
            WHERE cantidadExhibicion < 0
        )
        BEGIN
            THROW 50001, 'La cantidad en exhibición no puede ser negativa.', 1;
        END;

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        PRINT 'Venta registrada exitosamente con el ID: ' + CAST(@ventaId AS NVARCHAR);

    END TRY
    BEGIN CATCH
        -- Revertir en caso de error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE T_ActualizarVenta
    @noVenta INT, -- Número de venta para identificar la venta a actualizar
    @pagoEfectivo DECIMAL(18, 2) = NULL,
    @pagoTarjeta DECIMAL(18, 2) = NULL,
    @pagoMonedero DECIMAL(18, 2) = NULL,
    @iva DECIMAL(18, 2),
    @codigoMonedero NVARCHAR(MAX) = NULL, -- Parámetro opcional
    @tieneRedondeo BIT,
    @detalles DetalleVentaType READONLY -- Tipo de tabla como parámetro
AS
BEGIN
    SET XACT_ABORT ON; -- Garantiza la atomicidad
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Asignar valores predeterminados si los parámetros son NULL
        SET @pagoEfectivo = ISNULL(@pagoEfectivo, 0.00);
        SET @pagoTarjeta = ISNULL(@pagoTarjeta, 0.00);
        SET @pagoMonedero = ISNULL(@pagoMonedero, 0.00);

        -- 1. Verificar si la venta existe
        DECLARE @ventaId INT;

        SELECT @ventaId = id
        FROM Venta
        WHERE noVenta = @noVenta;

        IF @ventaId IS NULL
        BEGIN
            THROW 50000, 'La venta especificada no existe.', 1;
        END;

        -- 2. Actualizar los totales y demás datos de la venta
        DECLARE @monederoId INT = NULL;

        -- Obtener el ID del monedero si es necesario
        IF @codigoMonedero IS NOT NULL
        BEGIN
            SELECT @monederoId = id
            FROM Monedero
            WHERE codigoDeBarras = @codigoMonedero;

            IF @monederoId IS NULL
            BEGIN
                THROW 50000, 'El código de monedero proporcionado no existe.', 1;
            END;
        END;

        -- Actualizar la venta sin modificar cajaId ni empleadoId
        UPDATE Venta
        SET
            fechaRegistro = GETDATE(),
            iva = @iva,
            totalEfectivo = @pagoEfectivo,
            totalTarjeta = @pagoTarjeta,
            totalMonedero = @pagoMonedero,
            tieneRedondeo = @tieneRedondeo,
            monederoId = @monederoId
        WHERE id = @ventaId;

        -- 3. Gestionar los detalles de la venta y ajustar el inventario
        -- Obtener los detalles actuales de la venta
        DECLARE @detalleActual TABLE (
            codigo NVARCHAR(MAX),
            cantidad INT
        );

        INSERT INTO @detalleActual (codigo, cantidad)
        SELECT pi.codigo, dv.cantidad
        FROM DetalleVenta dv
        INNER JOIN ProductoInventario pi
            ON dv.productoInventarioId = pi.id
        WHERE dv.ventaId = @ventaId;

        -- Actualizar o eliminar los detalles existentes
        DECLARE @codigo NVARCHAR(MAX), @cantidad INT;

        -- Iterar sobre los detalles actuales
        DECLARE cur CURSOR FOR
        SELECT codigo, cantidad
        FROM @detalleActual;

        OPEN cur;
        FETCH NEXT FROM cur INTO @codigo, @cantidad;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Verificar si el código todavía está en los nuevos detalles
            IF NOT EXISTS (SELECT 1 FROM @detalles WHERE codigo = @codigo)
            BEGIN
                -- Eliminar detalle y devolver cantidad al inventario
                UPDATE ProductoInventario
                SET cantidadExhibicion = cantidadExhibicion + @cantidad
                WHERE codigo = @codigo;

                DELETE FROM DetalleVenta
                WHERE ventaId = @ventaId
                AND productoInventarioId = (SELECT id FROM ProductoInventario WHERE codigo = @codigo);
            END
            ELSE
            BEGIN
                -- Comparar cantidades y ajustar el inventario
                DECLARE @nuevaCantidad INT;

                SELECT @nuevaCantidad = cantidad
                FROM @detalles
                WHERE codigo = @codigo;

                IF @nuevaCantidad <> @cantidad
                BEGIN
                    -- Ajustar inventario
                    UPDATE ProductoInventario
                    SET cantidadExhibicion = cantidadExhibicion + (@cantidad - @nuevaCantidad)
                    WHERE codigo = @codigo;

                    -- Actualizar detalle de venta, incluyendo la ganancia directamente desde el total
                    UPDATE DetalleVenta
                    SET cantidad = @nuevaCantidad,
                        ganancia = (SELECT total FROM @detalles WHERE codigo = @codigo) -- Usamos el total proporcionado como ganancia
                    WHERE ventaId = @ventaId
                      AND productoInventarioId = (SELECT id FROM ProductoInventario WHERE codigo = @codigo);
                END;
            END;

            FETCH NEXT FROM cur INTO @codigo, @cantidad;
        END;

        CLOSE cur;
        DEALLOCATE cur;

        -- Insertar nuevos detalles
        INSERT INTO DetalleVenta (cantidad, precioVenta, ventaId, productoInventarioId, ganancia, codigo)
        SELECT
            dv.cantidad,
            dv.precio,
            @ventaId,
            pi.id,
            dv.total, -- Usamos el total proporcionado como ganancia
            dv.codigo
        FROM @detalles dv
        LEFT JOIN ProductoInventario pi
            ON dv.codigo = pi.codigo
        WHERE NOT EXISTS (
            SELECT 1 FROM @detalleActual WHERE codigo = dv.codigo
        );

        -- Ajustar inventario por los nuevos detalles
        UPDATE ProductoInventario
        SET cantidadExhibicion = cantidadExhibicion - dv.cantidad
        FROM @detalles dv
        INNER JOIN ProductoInventario pi
            ON dv.codigo = pi.codigo
        WHERE NOT EXISTS (
            SELECT 1 FROM @detalleActual WHERE codigo = dv.codigo
        );

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        PRINT 'Venta actualizada exitosamente con el ID: ' + CAST(@ventaId AS NVARCHAR);

    END TRY
    BEGIN CATCH
        -- Revertir en caso de error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE T_EliminarVenta
    @noVenta INT -- Número de venta a eliminar
AS
BEGIN
    SET XACT_ABORT ON; -- Garantiza la atomicidad
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Verificar si la venta existe
        DECLARE @ventaId INT;

        SELECT @ventaId = id
        FROM Venta
        WHERE noVenta = @noVenta;

        IF @ventaId IS NULL
        BEGIN
            THROW 50000, 'La venta especificada no existe.', 1;
        END;

        -- 2. Obtener los detalles de la venta
        DECLARE @detalles TABLE (
            productoInventarioId INT,
            cantidad INT,
            fechaCaducidad DATETIME2(7),
            esPerecedero BIT
        );

        INSERT INTO @detalles (productoInventarioId, cantidad, fechaCaducidad, esPerecedero)
        SELECT 
            pi.id,
            dv.cantidad,
            pi.fechaCaducidad,
            pi.esPerecedero
        FROM DetalleVenta dv
        INNER JOIN ProductoInventario pi
            ON dv.productoInventarioId = pi.id
        WHERE dv.ventaId = @ventaId;

        -- 3. Procesar los detalles
        DECLARE @productoInventarioId INT, @cantidad INT, @fechaCaducidad DATETIME2(7), @esPerecedero BIT;

        DECLARE cur CURSOR FOR
        SELECT productoInventarioId, cantidad, fechaCaducidad, esPerecedero
        FROM @detalles;

        OPEN cur;
        FETCH NEXT FROM cur INTO @productoInventarioId, @cantidad, @fechaCaducidad, @esPerecedero;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @esPerecedero = 1 AND @fechaCaducidad < GETDATE()
            BEGIN
                -- Registrar una merma
                INSERT INTO Merma (cantidad, descripcion, fechaRegistro, productoInventarioId)
                VALUES (
                    @cantidad,
                    'Producto caducado tras devolución',
                    GETDATE(),
                    @productoInventarioId
                );
            END
            ELSE
            BEGIN
                -- Devolver la cantidad al inventario
                UPDATE ProductoInventario
                SET cantidadExhibicion = cantidadExhibicion + @cantidad
                WHERE id = @productoInventarioId;
            END;

            FETCH NEXT FROM cur INTO @productoInventarioId, @cantidad, @fechaCaducidad, @esPerecedero;
        END;

        CLOSE cur;
        DEALLOCATE cur;

        -- 4. Eliminar los detalles de la venta
        DELETE FROM DetalleVenta
        WHERE ventaId = @ventaId;

        -- 5. Eliminar la venta
        DELETE FROM Venta
        WHERE id = @ventaId;

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        PRINT 'Venta eliminada exitosamente con el ID: ' + CAST(@ventaId AS NVARCHAR);

    END TRY
    BEGIN CATCH
        -- Revertir en caso de error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE [dbo].T_FinalizarPromocion
(
    @promocionId INT
)
AS
BEGIN
    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Quitar el promocionId de los productos en ProductoInventario
        UPDATE ProductoInventario
        SET promocionId = NULL
        WHERE promocionId = @promocionId;

        -- 2. Actualizar la fecha de finalización en PromocionVigencia
        UPDATE PromocionVigencia
        SET fechaFin = DATEADD(DAY, -1, GETDATE())
        WHERE promocionId = @promocionId;

        -- Confirmar la transacción si todo es exitoso
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, deshacer la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE PROCEDURE [dbo].T_FinalizarPromocionesExpiradas
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Obtener todos los promocionId cuya fechaFin ya pasó
        DECLARE @PromocionesExpiradas TABLE (promocionId INT);

        INSERT INTO @PromocionesExpiradas (promocionId)
        SELECT promocionId
        FROM PromocionVigencia
        WHERE fechaFin < CAST(GETDATE() AS DATE);

        -- Actualizar ProductoInventario removiendo los promocionId expirados
        UPDATE ProductoInventario
        SET promocionId = NULL
        WHERE promocionId IN (SELECT promocionId FROM @PromocionesExpiradas);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        -- En caso de error, lanzar el error para que sea registrado
        THROW;
    END CATCH;
END;
GO

-- CU-03 Registrar Producto
CREATE PROCEDURE T_RegistrarProductoInventario
    @noPedido NVARCHAR(MAX),      -- Número del pedido
    @codigoProducto NVARCHAR(MAX), -- Código del producto
    @nombreCategoria NVARCHAR(MAX), -- Nombre de la categoría
    @precioActual DECIMAL(18, 2), -- Precio actual del producto
    @fechaCaducidad DATE          -- Fecha de caducidad proporcionada
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar transacción
    BEGIN TRAN;

    BEGIN TRY
        -- Declarar variables locales
        DECLARE @idProducto INT;
        DECLARE @idCategoria INT;
        DECLARE @idPedido INT;
        DECLARE @cantidadBodega INT;
        DECLARE @idEstadoDisponible INT;

        -- Obtener el ID del estado "Disponible"
        SELECT @idEstadoDisponible = id
        FROM EstadoProducto
        WHERE nombre = 'Disponible';

        IF @idEstadoDisponible IS NULL
        BEGIN
            RAISERROR('El estado "Disponible" no existe en la tabla EstadoProducto.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener el ID del producto desde el código
        SELECT @idProducto = id
        FROM Producto
        WHERE codigo = @codigoProducto;

        IF @idProducto IS NULL
        BEGIN
            RAISERROR('El producto con el código proporcionado no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener el ID del pedido desde el número de pedido
        SELECT @idPedido = id
        FROM Pedido
        WHERE noPedido = @noPedido;

        IF @idPedido IS NULL
        BEGIN
            RAISERROR('El pedido con el número proporcionado no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener la cantidad desde DetallePedido
        SELECT 
            @cantidadBodega = DP.cantidad
        FROM DetallePedido DP
        WHERE DP.pedidoId = @idPedido AND DP.productoId = @idProducto;

        IF @cantidadBodega IS NULL
        BEGIN
            RAISERROR('No se encontró un detalle de pedido válido para el producto en el pedido proporcionado.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener el ID de la categoría desde el nombre
        SELECT @idCategoria = id
        FROM Categoria
        WHERE nombre = @nombreCategoria;

        IF @idCategoria IS NULL
        BEGIN
            RAISERROR('La categoría proporcionada no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Verificar si el producto ya existe en ProductoInventario
        IF EXISTS (SELECT 1 FROM ProductoInventario WHERE codigo = @codigoProducto)
        BEGIN
            -- Si ya existe, actualizar los campos relevantes
            UPDATE ProductoInventario
            SET 
                cantidadBodega = cantidadBodega + @cantidadBodega, -- Sumar a la cantidad existente
                precioActual = @precioActual,                      -- Actualizar el precio
                categoriaId = @idCategoria,                        -- Actualizar la categoría
                fechaCaducidad = @fechaCaducidad,                  -- Actualizar la fecha de caducidad
                estadoProductoId = @idEstadoDisponible             -- Actualizar el estado a "Disponible"
            WHERE 
                codigo = @codigoProducto;
        END
        ELSE
        BEGIN
            -- Si no existe, insertar un nuevo registro
            INSERT INTO ProductoInventario (
                codigo, 
                nombre, 
                descripcion, 
                cantidadBodega, 
                cantidadExhibicion, 
                precioActual, 
                fechaCaducidad, 
                esPerecedero, 
                esDevolvible, 
                unidadDeMedidaId, 
                categoriaId, 
                estadoProductoId
            )
            SELECT 
                P.codigo,
                P.nombre,
                P.descripcion,
                @cantidadBodega,         -- Cantidad en bodega del DetallePedido
                0,                       -- Cantidad en exhibición inicia en 0
                @precioActual,           -- Precio actual ingresado por el usuario
                @fechaCaducidad,         -- Fecha de caducidad proporcionada como parámetro
                P.esPerecedero,
                P.esDevolvible,
                P.unidadDeMedidaId,
                @idCategoria,            -- ID de la categoría obtenida dinámicamente
                @idEstadoDisponible      -- Estado inicial como "Disponible"
            FROM Producto P
            WHERE P.id = @idProducto;
        END

        -- Confirmar transacción
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Manejar errores y revertir transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        -- Propagar mensaje de error
        THROW;
    END CATCH
END;
GO


-- CU-03 Registrar producto
CREATE PROCEDURE T_ActualizarEstadoPedido
    @noPedido NVARCHAR(MAX),   -- Cambiar a noPedido como identificador principal
    @fechaEntrega DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar transacción
    BEGIN TRAN;

    BEGIN TRY
        -- Verificar si el estado "Entregado" existe en EstadoPedido
        DECLARE @idEstadoEntregado INT;
        SELECT @idEstadoEntregado = id 
        FROM EstadoPedido 
        WHERE nombre = 'Entregado';

        IF @idEstadoEntregado IS NULL
        BEGIN
            RAISERROR('El estado "Entregado" no existe en la tabla EstadoPedido.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Verificar si el pedido existe
        IF NOT EXISTS (SELECT 1 FROM Pedido WHERE noPedido = @noPedido)
        BEGIN
            RAISERROR('El pedido con el número proporcionado no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Actualizar el estado del pedido y la fecha de entrega
        UPDATE Pedido
        SET estadoPedidoId = @idEstadoEntregado, -- Cambiar idEstadoPedido por estadoPedidoId
            fechaEntrega = @fechaEntrega
        WHERE noPedido = @noPedido;

        -- Confirmar transacción
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        -- Propagar el mensaje de error
        THROW;
    END CATCH
END;
GO

-- CU-05 Editar Producto
CREATE PROCEDURE T_EditarProductoInventario
    @codigoProducto NVARCHAR(MAX),      -- Código del producto
    @descripcion NVARCHAR(MAX),         -- Descripción actualizada
    @cantidadBodega INT,                -- Nueva cantidad en bodega
    @cantidadExhibicion INT,            -- Nueva cantidad en exhibición
    @precioActual DECIMAL(18, 2),       -- Nuevo precio actual
    @fechaCaducidad NVARCHAR(MAX),      -- Nueva fecha de caducidad (como string para validar)
    @nombreCategoria NVARCHAR(MAX),     -- Nueva categoría (nombre)
    @nombreUnidadMedida NVARCHAR(MAX),  -- Nueva unidad de medida (nombre)
    @esPerecedero BIT,                  -- Indicador si es perecedero
    @esDevolvible BIT                   -- Indicador si es devolvible
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar transacción
    BEGIN TRAN;

    BEGIN TRY
        -- Validar formato de la fecha de caducidad
        DECLARE @fechaValida DATE;
        BEGIN TRY
            SET @fechaValida = CAST(@fechaCaducidad AS DATE); -- Validar conversión de fecha
        END TRY
        BEGIN CATCH
            RAISERROR('La fecha de caducidad proporcionada no es válida.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END CATCH

        -- Verificar si el producto existe en ProductoInventario
        IF NOT EXISTS (SELECT 1 FROM ProductoInventario WHERE codigo = @codigoProducto)
        BEGIN
            RAISERROR('El producto no existe en ProductoInventario.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener el ID de la categoría desde su nombre
        DECLARE @idCategoria INT;
        SELECT @idCategoria = id 
        FROM Categoria
        WHERE nombre = @nombreCategoria;

        IF @idCategoria IS NULL
        BEGIN
            RAISERROR('La categoría proporcionada no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Obtener el ID de la unidad de medida desde su nombre
        DECLARE @idUnidadMedida INT;
        SELECT @idUnidadMedida = id 
        FROM UnidadDeMedida
        WHERE nombre = @nombreUnidadMedida;

        IF @idUnidadMedida IS NULL
        BEGIN
            RAISERROR('La unidad de medida proporcionada no existe.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Actualizar ProductoInventario con los nuevos datos
        UPDATE ProductoInventario
        SET 
            descripcion = @descripcion,
            cantidadBodega = @cantidadBodega,
            cantidadExhibicion = @cantidadExhibicion,
            precioActual = @precioActual,
            fechaCaducidad = @fechaValida,  -- Actualizamos la fecha de caducidad
            categoriaId = @idCategoria,     -- ID de la nueva categoría
            unidadDeMedidaId = @idUnidadMedida, -- ID de la nueva unidad de medida
            esPerecedero = @esPerecedero,   -- Indicador perecedero
            esDevolvible = @esDevolvible    -- Indicador devolvible
        WHERE 
            codigo = @codigoProducto;

        -- Confirmar transacción
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Revertir cambios en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        -- Propagar error
        THROW;
    END CATCH
END;
GO

-- CU-07 Registrar proveedor
--Tipo Tabla para meter una lista de productos
CREATE TYPE TipoProducto AS TABLE (
    Codigo NVARCHAR(50),
    Descripcion NVARCHAR(255),
    EsDevolvible NVARCHAR(10),
    EsPerecedero NVARCHAR(10),
    Nombre NVARCHAR(100),
    UnidadDeMedida NVARCHAR(50)
);
GO

-- Procedimiento para reigstrar proveedor y productos
CREATE PROCEDURE T_RegistrarProveedorYProductos
    @RFC NVARCHAR(13),
    @Nombre NVARCHAR(100),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Productos TipoProducto READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar proveedor
        DECLARE @ProveedorId INT;

        INSERT INTO Proveedor (RFC, Nombre, Correo, Telefono, estadoProveedor)
        VALUES (@RFC, @Nombre, @Correo, @Telefono, 1);

        -- Obtener el ID del proveedor recién insertado
        SET @ProveedorId = SCOPE_IDENTITY();

        -- Insertar productos asociados
        INSERT INTO Producto (Codigo, Descripcion, EsDevolvible, EsPerecedero, Nombre, ProveedorId, UnidadDeMedidaId)
        SELECT 
            p.Codigo,
            p.Descripcion,
            CASE 
                WHEN p.EsDevolvible = 'no' THEN 0
                ELSE 1 
            END AS EsDevolvible,
            CASE 
                WHEN p.EsPerecedero = 'no' THEN 0
                ELSE 1 
            END AS EsPerecedero,
            p.Nombre,
            @ProveedorId,
            (SELECT Id FROM UnidadDeMedida WHERE Nombre = p.UnidadDeMedida)
        FROM @Productos p;

        -- Confirmar transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- CU-11 Registrar empleado
CREATE PROCEDURE T_RegistrarEmpleado
    @RFC NVARCHAR(13),
    @Nombre NVARCHAR(255),
    @ApellidoP NVARCHAR(255),
    @ApellidoM NVARCHAR(255),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(10),
    @Puesto NVARCHAR(100)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Declarar variables locales
        DECLARE @NumeroEmpleado NVARCHAR(10);
        DECLARE @Password NVARCHAR(MAX);
        DECLARE @UltimoNumero INT;
        DECLARE @PuestoID INT;
        
        -- Buscamos el ID del puesto correspondiente en la tabla Puesto
        SELECT @PuestoID = ID
        FROM Puesto
        WHERE Nombre = @Puesto;

        -- Si no se encuentra el puesto, se sale y se lanza un error
        IF @PuestoID IS NULL
        BEGIN
            RAISERROR('El puesto especificado no existe.', 16, 1);
            RETURN;
        END
        
        -- Validar si el RFC ya está registrado
        IF EXISTS (SELECT 1 FROM Empleado WHERE RFC = @RFC)
        BEGIN
            THROW 50001, 'El RFC proporcionado ya está registrado.', 1;
        END

        -- Generar el número de empleado
        SELECT @UltimoNumero = ISNULL(MAX(CAST(SUBSTRING(noEmpleado, 2, LEN(noEmpleado)) AS INT)), 0)
        FROM Empleado;

        SET @NumeroEmpleado = CONCAT('E', FORMAT(@UltimoNumero + 1, '000000'));

        -- Generar contraseña
        SET @Password = CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', @RFC), 1);

        -- Insertar el empleado en la tabla
        INSERT INTO Empleado ( RFC, noEmpleado, Nombre, apellidoPaterno, apellidoMaterno, Correo, Telefono, Password, estado, puestoId)
        VALUES (@RFC, @NumeroEmpleado, @Nombre, @ApellidoP, @ApellidoM, @Correo, @Telefono, @Password, 1, @PuestoID);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--CU 28 "Registrar Pedido a Proveedor"
CREATE TYPE TipoTablaPedidoDetalle AS TABLE
(
    NombreProducto NVARCHAR(60), 
    Cantidad INT,
	PrecioCompra DECIMAL
);
GO

CREATE PROCEDURE T_RegistrarPedido
    @ProductosDetalle TipoTablaPedidoDetalle READONLY 
AS
BEGIN
    BEGIN TRANSACTION; 

    BEGIN TRY
        DECLARE @PedidoId INT; 
        DECLARE @NoPedido NVARCHAR(20); 
        DECLARE @FechaPedido DATE = GETDATE(); 
        DECLARE @EstadoPedidoId INT = (SELECT TOP 1 id FROM EstadoPedido WHERE nombre = 'Pendiente'); 

        INSERT INTO Pedido (fechaPedido, estadoPedidoId)
        VALUES (@FechaPedido, @EstadoPedidoId);

        -- Recuperar el ID del pedido recién insertado
        SET @PedidoId = SCOPE_IDENTITY();

        SET @NoPedido = CONCAT('PED', FORMAT(@PedidoId, 'D6')); 

        -- Actualizar el número de pedido en la tabla
        UPDATE Pedido
        SET noPedido = @NoPedido
        WHERE id = @PedidoId;

        INSERT INTO DetallePedido (pedidoId, productoId, cantidad, precioCompra)
        SELECT 
            @PedidoId, 
            p.id AS productoId,
            pd.Cantidad,
            pd.PrecioCompra
        FROM 
            @ProductosDetalle pd
        INNER JOIN 
            Producto p ON p.nombre = pd.NombreProducto;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH

        ROLLBACK TRANSACTION;

        --Lanzar el error por si acaso
        THROW;
    END CATCH;
END;
GO

-- 4. disparadores

-- 5. jobs
-- para los jobs se debe de utilizar el usuario SAMS.Data.Admin
-- los jobs deben de ser ejecutados hasta el ultimo de el script
-- FAVOR DE PONER PRIMERO EL USUARIO SAMS.Data.Admin !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
-- Puede marcar error si ya ejecutó anteriormente una vez (no importa si borraste la BD)
USE [msdb];
GO
-- 1. Crear el Job
EXEC sp_add_job 
    @job_name = N'RN_FinalizarPromocionesExpiradas', 
    @enabled = 1, 
    @description = N'Job para finalizar promociones expiradas.', 
    @owner_login_name = N'SAMS.Data.Admin';
-- 2. Crear un paso del Job
EXEC sp_add_jobstep 
    @job_name = N'RN_FinalizarPromocionesExpiradas', 
    @step_name = N'Ejecutar T_FinalizarPromocionesExpiradas', 
    @subsystem = N'TSQL', 
    @command = N'EXEC [SAMS.Data].[dbo].[T_FinalizarPromocionesExpiradas];', 
    @retry_attempts = 0, 
    @retry_interval = 0, 
    @on_success_action = 1, 
    @on_fail_action = 2;
-- 3. Crear un horario para el Job
EXEC sp_add_jobschedule 
    @job_name = N'RN_FinalizarPromocionesExpiradas', 
    @name = N'Horario diario a las 12 AM', 
    @freq_type = 4,  -- Frecuencia diaria
    @freq_interval = 1, 
    @active_start_time = 0;  -- 12 AM
-- 4. Habilitar el Job
EXEC sp_add_jobserver 
    @job_name = N'RN_FinalizarPromocionesExpiradas', 
    @server_name = N'(local)';
GO
