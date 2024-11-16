-- 1. EstadoProducto
INSERT INTO EstadoProducto (nombre) VALUES
('Disponible'),
('Agotado');
GO

-- 2. Categoria
INSERT INTO Categoria (nombre, estado) VALUES
('Lácteos', 1),
('Panadería', 1),
('Bebidas', 1),
('Aseo Personal', 1);
GO

-- 3. UnidadDeMedida
INSERT INTO UnidadDeMedida (nombre) VALUES
('Litro'),
('Kilogramo'),
('Gramo'),
('Pieza');
GO

-- 4. Proveedor
INSERT INTO Proveedor (rfc, nombre, correo, telefono, estadoProveedor) VALUES
('XAXX010101000', 'Proveedora de Lacteos S.A.', 'contacto@lacteos.com', '5551234567', 1),
('GGM7508243P0', 'Grupo Bimbo S.A.B. de C.V.', 'ventas@bimbo.com', '5552345678', 1),
('PRC9802164R1', 'Productos Cárnicos S.A.', 'info@carnicos.com', '5553456789', 1),
('ALA030507H2A', 'Alpura S.A. de C.V.', 'servicio@alpura.com', '5554567890', 1),
('KIM091001CV1', 'Kimberly-Clark S.A.', 'soporte@kimberly.com', '5555678901', 1);
GO

-- 5. Promocion
INSERT INTO Promocion (nombre, porcentajeDescuento, cantMaxima, cantMinima) VALUES
('2 X 1', 100, 2, 2),
('M   itad de precio', 50, 1000, 1);
GO

-- 6. Monedero
INSERT INTO Monedero (codigoDeBarras, saldo, telefono, nombre, apellidoPaterno, apellidoMaterno) VALUES
('000000000001', 100.50, '5551234567', 'Lucas', 'Viveros', 'Gutierrez'),
('000000000002', 250.75, '5552345678', 'Salma', 'Tinoco', 'Alarcón'),
('000000000003', 50.00, '5553456789', 'Ramiro', 'Hernández', 'Quiróz'),
('000000000004', 300.00, '5554567890', 'Viviana', 'Fernández', 'Linares'),
('000000000005', 125.20, '5555678901', 'Ulises', 'Zamora', 'Aguilar'),
('000000000006', 75.00, '5556789012', 'Yolanda', 'Villalobos', 'García'),
('000000000007', 200.00, '5557890123', 'Zaira', 'Villanueva', 'González'),
('000000000008', 150.00, '5558901234', 'Ximena', 'Villarreal', 'Guzmán'),
('000000000009', 100.00, '5559012345', 'Wendy', 'Villegas', 'Guzmán'),
('000000000010', 50.00, '5550123456', 'Víctor', 'Villaseñor', 'Guzmán');
GO

-- 7. EstadoPedido
INSERT INTO EstadoPedido (nombre) VALUES
('Pendiente'),
('Entregado'),
('Cancelado');
GO

-- 8. Puesto
INSERT INTO Puesto (nombre) VALUES
('Gerente'),
('Cajero'),
('Almacen'),
('Contador');
GO

-- 9. Empleado
-- El empleado va a jalar JPL001 ese numero es de acuer a las uniciales de el nombre completo de la persona
INSERT INTO Empleado (rfc, noempleado, nombre, apellidoPaterno, apellidoMaterno, correo, password, telefono, puestoId) VALUES
('GARC840214HNL', 'E001', 'Juan', 'Pérez', 'López', 'juan.perez@gmail.com', 'contrasena123', '5551234567', 1),
('MALO920512HNL', 'E002', 'María', 'López', 'Sánchez', 'maria.lopez@gmail.com', 'contrasena123', '5552345678', 2),
('SACH950814HNL', 'E003', 'Carlos', 'Sánchez', 'Pérez', 'carlos.sanchez@gmail.com', 'contrasena123', '5553456789', 3);
GO

-- 10. Caja
INSERT INTO Caja (noCaja, horaDeCorte) VALUES
('01', '2024-10-18 22:00:00'),
('02', '2024-10-18 22:00:00'),
('03', '2024-10-18 22:00:00');
GO

-- 11. Producto
INSERT INTO Producto (codigo, descripcion, esDevolvible, esPerecedero, nombre, proveedorId, unidadDeMedidaId) VALUES
('7501000111110', 'Leche entera pasteurizada Alpura', 1, 1, 'Leche Entera Alpura 1L', 1, 1),
('7501000122223', 'Pan de caja blanco Bimbo', 1, 0, 'Pan Blanco Bimbo 680g', 2, 4),
('7501000133336', 'Refresco Coca-Cola 2 litros', 1, 0, 'Coca-Cola 2L', 3, 1);
GO

-- 12. ProductoInventario
INSERT INTO ProductoInventario (codigo, nombre, descripcion, cantidadBodega, cantidadExhibicion, precioActual, esPerecedero, esDevolvible, ubicacion, unidadDeMedidaId, categoriaId, estadoProductoId, promocionId) VALUES
('7501000111110', 'Leche Entera Alpura 1L', 'Leche entera pasteurizada Alpura', 150, 200, 18.50, 1, 1, 1, 1, 1, 1, 1),
('7501000122223', 'Pan Blanco Bimbo 680g', 'Pan de caja blanco Bimbo', 120, 50, 32.00, 0, 1, 1, 4, 2, 1, 2),
('7501000133336', 'Coca-Cola 2L', 'Refresco Coca-Cola 2 litros', 200, 100, 29.00, 0, 1, 1, 1, 3, 1, 1);
GO

-- 13. Pedido
INSERT INTO Pedido (noPedido, fechaPedido, fechaEntrega, estadoPedidoId) VALUES
(1001, '2024-10-01', '2024-10-05', 1),
(1002, '2024-10-02', '2024-10-06', 1),
(1003, '2024-10-03', '2024-10-07', 1);
GO

-- 14. DetallePedido
INSERT INTO DetallePedido (cantidad, precioCompra, pedidoId, productoId, fechaCaducidad) VALUES
(50, 15.00, 1, 1, '2024-12-10'),
(100, 30.00, 2, 2, '2025-01-20'),
(75, 25.00, 3, 3, '2024-11-05');    
GO

-- 15. Merma
INSERT INTO Merma (cantidad, descripcion, fechaRegistro, productoInventarioId) VALUES
(10, 'Producto dañado durante el traslado', '2024-10-12', 1),
(5, 'Producto caducado en almacén', '2024-10-15', 2),
(20, 'Producto con defecto de fabricación', '2024-10-18', 3);
GO

-- 16. Venta
INSERT INTO Venta (noVenta, fechaRegistro, iva, totalEfectivo, totalTarjeta, totalMonedero, cajaId, monederoId, empleadoId) VALUES
(1001, '2024-10-15 10:30:00', 0.50, 5.00, 0.00, 0.50, 1, 1, 1),
(1002, '2024-10-15 12:45:00', 0.70, 6.00, 1.70, 0.00, 2, 2, 2),
(1003, '2024-10-15 15:00:00', 1.00, 5.00, 6.00, 0.00, 3, 3, 3);
GO

-- 17. DetalleVenta
INSERT INTO DetalleVenta (cantidad, precioVenta, ventaId, productoInventarioId, ganancia) VALUES
(2, 37.00, 1, 1, 5.00),
(1, 32.00, 2, 2, 7.00),
(3, 87.00, 3, 3, 10.00);
GO

-- 18. PromocionVigencia
INSERT INTO PromocionVigencia (fechaInicio, fechaFin, promocionId) VALUES
('2024-12-01', '2024-12-25', 1),
('2024-11-01', '2024-11-30', 2);
GO