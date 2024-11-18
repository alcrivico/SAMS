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
    p.porcentajeDescuento
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

CREATE VIEW V_Ventas AS
SELECT
    dv.precioVenta,
    dv.cantidad,
    v.noVenta,
    v.fechaRegistro,
    c.noCaja,
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombreEmpleado
FROM
    DetalleVenta dv
INNER JOIN
    Venta v
    ON
    dv.ventaId = v.id
INNER JOIN
    Caja c
    ON
    v.cajaId = c.id
INNER JOIN
    Empleado e
    ON
    v.empleadoId = e.id
GO

CREATE VIEW V_DetalleVentas AS
SELECT
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

CREATE VIEW V_Promocion AS
SELECT
    p.id,
	p.nombre,
	p.porcentajeDescuento,
	pv.fechaInicio,
	pv.fechaFin
FROM
	Promocion p
INNER JOIN
	PromocionVigencia pv
	ON
	p.id = pv.promocionId
GO


CREATE VIEW V_Proveedores AS
SELECT
    p.nombre,
    p.rfc,
    p.estadoProveedor
FROM
    Proveedor p
GO

CREATE VIEW V_Producto AS
SELECT
    p.nombre,
    p.codigo,
    p.descripcion
FROM 
    Producto p
GO

CREATE VIEW V_Empleados AS
SELECT
    CONCAT(e.nombre, ' ', e.apellidoPaterno, ' ', e.apellidoMaterno) AS nombre,
    e.rfc,
    p.nombre AS puesto
FROM
    Empleado e
INNER JOIN
    Puesto p
    ON
    e.puestoId = p.id
GO

CREATE VIEW V_EmpleadoDetalle AS
SELECT
    e.nombre,
    e.apellidoPaterno,
    e.apellidoMaterno,
    e.rfc,
    e.noempleado,
    e.correo,
    p.nombre AS puesto
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
CREATE VIEW V_PedidosPendientes
AS
SELECT 
    P.noPedido AS NoPedido,
    P.fechaEntrega AS FechaEntrega,
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
    PED.noPedido AS NumeroPedido,        -- Número del pedido
    P.codigo AS CodigoProducto,          -- Código del producto
    P.nombre AS NombreProducto,          -- Nombre del producto
    DP.cantidad AS Cantidad,             -- Cantidad del detalle del pedido
    DP.precioCompra AS PrecioCompra      -- Precio de compra del producto
FROM 
    DetallePedido DP
INNER JOIN 
    Producto P ON DP.productoId = P.id
INNER JOIN 
    Pedido PED ON DP.pedidoId = PED.id;  -- Relación con la tabla Pedido
GO

-- CU-04 Ver productos
CREATE VIEW V_ProductosRegistrados
AS
SELECT 
    PI.nombre AS NombreProducto,                              -- Nombre del producto
    CONCAT(PI.cantidadBodega + PI.cantidadExhibicion, ' ', UM.nombre) AS Cantidad, -- Total cantidad con unidad
    PI.precioActual AS PrecioActual,                         -- Precio actual del producto
    CAT.nombre AS NombreCategoria                            -- Nombre de la categoría
FROM 
    ProductoInventario PI
INNER JOIN 
    UnidadDeMedida UM ON PI.unidadDeMedidaId = UM.id         -- Relación con unidad de medida
INNER JOIN 
    Categoria CAT ON PI.categoriaId = CAT.id;               -- Relación con categoría
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
    DP.fechaCaducidad AS FechaCaducidad,                  -- Fecha de caducidad (de DetallePedido)
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
LEFT JOIN 
    DetallePedido DP ON DP.productoId = PI.id;            -- Relación con DetallePedido (puede no existir)
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


-- 1. Crear tipo de tabla para lista de IDs si no existe
IF TYPE_ID('dbo.productoInventarioIdList') IS NULL
    CREATE TYPE dbo.productoInventarioIdList AS TABLE (productoInventarioId INT);
GO

-- 2. Procedimiento T_EditarPromocion con formato solicitado
CREATE PROCEDURE [dbo].T_EditarPromocion
(
    @promocionId INT,
    @nombre NVARCHAR(100),
    @porcentajeDescuento INT,
    @fechaInicio DATE,
    @fechaFin DATE,
    @productoInventarioIdList dbo.productoInventarioIdList READONLY -- Usar el tipo de tabla correcto
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

        -- 3. Obtener los productos que actualmente tienen la promoción
        DECLARE @CurrentProductos TABLE (productoInventarioId INT);
        INSERT INTO 
            @CurrentProductos
        SELECT 
            id 
        FROM 
            ProductoInventario 
        WHERE 
            promocionId = @promocionId;

        -- 4. Actualizar ProductoInventario según el arreglo proporcionado

        -- Agregar promocionId a los productos en el arreglo pero no en la tabla
        UPDATE ProductoInventario
        SET promocionId = @promocionId
        WHERE 
            id IN (SELECT productoInventarioId FROM @productoInventarioIdList) -- Usar la variable correcta
        AND id NOT IN (SELECT productoInventarioId FROM @CurrentProductos);

        -- Quitar promocionId de los productos en la tabla pero no en el arreglo
        UPDATE 
            ProductoInventario
        SET promocionId = NULL
        WHERE 
            id IN (SELECT productoInventarioId FROM @CurrentProductos)
        AND 
            id NOT IN (SELECT productoInventarioId FROM @productoInventarioIdList); -- Usar la variable correcta

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
    @noPedido NVARCHAR(MAX),   -- Número del pedido
    @codigoProducto NVARCHAR(MAX), -- Código del producto
    @idCategoria INT,          -- ID de la categoría
    @precioActual DECIMAL(18, 2) -- Precio actual del producto
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar transacción
    BEGIN TRAN;

    BEGIN TRY
        -- Verificar que el pedido y producto estén relacionados en DetallePedido
        DECLARE @cantidadBodega INT;

        SELECT 
            @cantidadBodega = DP.cantidad
        FROM 
            DetallePedido DP
        INNER JOIN 
            Pedido PED ON DP.pedidoId = PED.id
        INNER JOIN 
            Producto P ON DP.productoId = P.id
        WHERE 
            PED.noPedido = @noPedido AND P.codigo = @codigoProducto;

        -- Si no se encuentra cantidad, abortar
        IF @cantidadBodega IS NULL
        BEGIN
            RAISERROR('El producto no está asociado al pedido proporcionado.', 16, 1);
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
                categoriaId = @idCategoria                         -- Actualizar la categoría
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
                P.esPerecedero,
                P.esDevolvible,
                P.unidadDeMedidaId,
                @idCategoria,            -- Categoría ingresada por el usuario
                1                        -- Estado inicial por defecto
            FROM Producto P
            WHERE P.codigo = @codigoProducto;
        END

        -- Confirmar transacción
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Manejar errores y revertir cambios
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
    @descripcion NVARCHAR(MAX),         -- Nueva descripción
    @cantidadBodega INT,                -- Nueva cantidad en bodega
    @cantidadExhibicion INT,            -- Nueva cantidad en exhibición
    @precioActual DECIMAL(18, 2),       -- Nuevo precio actual
    @fechaCaducidad DATE,               -- Nueva fecha de caducidad
    @idCategoria INT,                   -- Nueva categoría
    @idUnidadMedida INT,                -- Nueva unidad de medida
    @esPerecedero BIT,                  -- Es perecedero
    @esDevolvible BIT                   -- Es devolvible
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar transacción
    BEGIN TRAN;

    BEGIN TRY
        -- Verificar si el producto existe en ProductoInventario
        IF NOT EXISTS (SELECT 1 FROM ProductoInventario WHERE codigo = @codigoProducto)
        BEGIN
            RAISERROR('El producto no existe en ProductoInventario.', 16, 1);
            ROLLBACK TRAN;
            RETURN;
        END

        -- Actualizar datos en ProductoInventario
        UPDATE ProductoInventario
        SET 
            descripcion = @descripcion,
            cantidadBodega = @cantidadBodega,
            cantidadExhibicion = @cantidadExhibicion,
            precioActual = @precioActual,
            categoriaId = @idCategoria,
            unidadDeMedidaId = @idUnidadMedida,
            esPerecedero = @esPerecedero,
            esDevolvible = @esDevolvible
        WHERE 
            codigo = @codigoProducto;

        -- Verificar si hay un DetallePedido relacionado para actualizar fecha de caducidad
        IF EXISTS (SELECT 1 FROM DetallePedido DP
                   INNER JOIN ProductoInventario PI ON DP.productoId = PI.id
                   WHERE PI.codigo = @codigoProducto)
        BEGIN
            UPDATE DP
            SET fechaCaducidad = @fechaCaducidad
            FROM DetallePedido DP
            INNER JOIN ProductoInventario PI ON DP.productoId = PI.id
            WHERE PI.codigo = @codigoProducto;
        END

        -- Confirmar transacción
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Revertir cambios en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        -- Propagar mensaje de error
        THROW;
    END CATCH
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
