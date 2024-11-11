-- 1. index

-- 2. vistas
CREATE VIEW V_ProductoInventario AS
SELECT
	id,
	nombre,
	cantidadBodega, 
	cantidadExhibicion,
	precioActual,
	ubicacion
FROM 
	ProductoInventario
GO

CREATE VIEW V_Pedido AS
SELECT 
	pr.nombre,
	p.NoPedido,
	p.FechaPedido,
	p.FechaEntrega
FROM
	Pedido p
INNER JOIN
	DetallePedido dp 
	ON
	p.id = dp.pedidoId
INNER JOIN
	Producto pr
	ON
	dp.productoId = pr.id
GO

CREATE VIEW V_Promocion AS
SELECT
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

-- 3. procedimientos almacenados
-- funciones listas
CREATE FUNCTION [dbo].FL_TotalDetallesVenta (
	@idVenta INT
)
RETURNS decimal(8,2)
AS
BEGIN
	DECLARE @total decimal(8,2);

	SELECT 
		@total = SUM(cantidad * precioVenta)
	FROM
		DetalleVenta db
	WHERE
		ventaId = @idVenta;

	RETURN ISNULL(@total, 0);
END;
GO

-- funciones escalares

-- procedimientos almacenados
CREATE PROCEDURE [dbo].SP_ReporteVenta
AS
BEGIN
	--Tabla temporal
	CREATE TABLE #TempVentas (
		noVenta INTEGER,
		fechaRegistro DATETIME,
		total DECIMAL(8, 2),
		noCaja VARCHAR(255),
		nombre NVARCHAR(100)
	);

	INSERT INTO 
		#TempVentas (noVenta, fechaRegistro, total, noCaja, nombre)
	SELECT
		v.noVenta,
		v.fechaRegistro,
		dbo.FL_TotalDetallesVenta(v.id) AS total,
		c.noCaja,
		(e.nombre + ' ' + e.apellidoPaterno + ' ' + e.apellidoMaterno) AS nombre
	FROM 
		Venta v
	INNER JOIN 
		Empleado e ON e.id = v.empleadoId
	INNER JOIN 
		Caja c ON c.id = v.cajaId;

	--resultado
	SELECT 
		noVenta,
		fechaRegistro,
		total,
		noCaja,
		nombre
	FROM 
		#TempVentas
	WHERE 
		total > 0
	ORDER BY
		noCaja;

	DROP TABLE #TempVentas;
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
	@idProductoInventario INT
)
AS
BEGIN
	-- Iniciar la transacción
	BEGIN TRANSACTION;

	BEGIN TRY
		-- 1. Insertar en la tabla Promocion
		DECLARE @idPromocion INT;

		INSERT INTO 
			Promocion (nombre, porcentajeDescuento, cantMaxima, cantMinima)
		VALUES 
			(@nombre, @porcentajeDescuento, @cantMaxima, @cantMinima);

		-- Obtener el ID de la promoción insertada
		SET @idPromocion = SCOPE_IDENTITY();

		-- 2. Insertar en la tabla PromocionVigencia
		INSERT INTO 
			PromocionVigencia (fechaInicio, fechaFin, promocionId)
		VALUES 
			(@fechaInicio, @fechaFin, @idPromocion);

		-- 3. Actualizar la tabla ProductoInventario con el idPromocion
		UPDATE 
			ProductoInventario
		SET promocionId = @idPromocion
		WHERE 
			id = @idProductoInventario;

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
IF TYPE_ID('dbo.ProductoInventarioIDList') IS NULL
    CREATE TYPE dbo.ProductoInventarioIDList AS TABLE (productoInventarioId INT);
GO

-- 2. Procedimiento T_EditarPromocion con formato solicitado
CREATE PROCEDURE [dbo].T_EditarPromocion
(
    @idPromocion INT,
    @nombre NVARCHAR(100),
    @porcentajeDescuento INT,
    @fechaInicio DATE,
    @fechaFin DATE,
    @idProductoInventarioList dbo.ProductoInventarioIDList READONLY
)
AS
BEGIN
    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Comparar y actualizar Promocion
        IF EXISTS (SELECT 1 FROM Promocion WHERE id = @idPromocion AND (nombre <> @nombre OR porcentajeDescuento <> @porcentajeDescuento))
        BEGIN
            UPDATE 
                Promocion
            SET 
                nombre = @nombre,
                porcentajeDescuento = @porcentajeDescuento
            WHERE 
                id = @idPromocion;
        END

        -- 2. Comparar y actualizar PromocionVigencia
        IF EXISTS 
            (SELECT 
                1 
            FROM 
                PromocionVigencia 
            WHERE 
                promocionId = @idPromocion 
            AND 
                (fechaInicio <> @fechaInicio OR fechaFin <> @fechaFin))
        BEGIN
            UPDATE 
                PromocionVigencia
            SET 
                fechaInicio = @fechaInicio,
                fechaFin = @fechaFin
            WHERE 
                promocionId = @idPromocion;
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
            promocionId = @idPromocion;

        -- 4. Actualizar ProductoInventario según el arreglo proporcionado

        -- Agregar idPromocion a los productos en el arreglo pero no en la tabla
        UPDATE ProductoInventario
        SET promocionId = @idPromocion
        WHERE 
            id IN (SELECT productoInventarioId FROM @idProductoInventarioList)
        AND id NOT IN (SELECT productoInventarioId FROM @CurrentProductos);

        -- Quitar idPromocion de los productos en la tabla pero no en el arreglo
        UPDATE 
            ProductoInventario
        SET promocionId = NULL
        WHERE 
            id IN (SELECT productoInventarioId FROM @CurrentProductos)
        AND 
            id NOT IN (SELECT productoInventarioId FROM @idProductoInventarioList);

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
    @idPromocion INT
)
AS
BEGIN
    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Eliminar idPromocion de los productos asociados en ProductoInventario
        UPDATE ProductoInventario
        SET promocionId = NULL
        WHERE promocionId = @idPromocion;

        -- 2. Actualizar fechaFin en PromocionVigencia
        UPDATE PromocionVigencia
        SET fechaFin = DATEADD(DAY, -1, GETDATE())
        WHERE promocionId = @idPromocion;

        -- Confirmar la transacción si todo es exitoso
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

CREATE PROCEDURE [dbo].T_FinalizarPromocionesExpiradas
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Obtener todos los idPromocion cuya fechaFin ya pasó
        DECLARE @PromocionesExpiradas TABLE (promocionId INT);

        INSERT INTO @PromocionesExpiradas (promocionId)
        SELECT promocionId
        FROM PromocionVigencia
        WHERE fechaFin < CAST(GETDATE() AS DATE);

        -- Actualizar ProductoInventario removiendo los idPromocion expirados
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
