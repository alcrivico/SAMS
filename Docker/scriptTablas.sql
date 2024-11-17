-- 1. Crear el inicio de sesión para el usuario en el nivel del servidor
USE [master];
GO
CREATE LOGIN [SAMS.Data.Admin] 
WITH PASSWORD = 'nZrYF&jArpj%f6aBAtE#';
GO
-- 2. Crear el usuario en la base de datos SAMS.Data
USE [SAMS.Data];
GO
CREATE USER [SAMS.Data.Admin] 
FOR LOGIN [SAMS.Data.Admin];
GO
-- 3. Otorgar permisos de owner solo para la base de datos SAMS.Data
ALTER ROLE db_owner ADD MEMBER [SAMS.Data.Admin];
GO
-- 4. Cambiar al contexto de la base de datos msdb
USE [msdb];
GO
-- 5. Crear el usuario en la base de datos msdb
CREATE USER [SAMS.Data.Admin] 
FOR LOGIN [SAMS.Data.Admin];
GO
-- 6. Agregar el usuario al rol SQLAgentUserRole en msdb
EXEC sp_addrolemember 
    @rolename = N'SQLAgentUserRole', 
    @membername = N'SAMS.Data.Admin';  
GO

USE [SAMS.Data];
GO