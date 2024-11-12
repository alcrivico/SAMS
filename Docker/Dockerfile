# Usamos la imagen oficial de SQL Server 2022 para Linux
FROM mcr.microsoft.com/mssql/server:2022-latest

# Configuración de variables de entorno para aceptar la EULA y establecer la contraseña del usuario sa
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=StrongPassword123!
ENV MSSQL_PID=Express

# Cambiar a usuario root para permitir la instalación de paquetes
USER root

# Instalar wait-for-it (si no lo tienes) para esperar a que SQL Server esté listo
RUN apt-get update && apt-get install -y netcat

# Crear el directorio de trabajo y copiar todos los scripts al contenedor
WORKDIR /usr/src/app
COPY . .

# Exponer el puerto interno 1433 para conexiones SQL Server
EXPOSE 1433

# Ejecutar SQL Server y luego los scripts en el orden adecuado
CMD /opt/mssql/bin/sqlservr & sleep 20 && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -d master -i /usr/src/app/dbo.sql -C && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -d SAMS.Data -i /usr/src/app/scriptDatos.sql -C && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -d SAMS.Data -i /usr/src/app/scriptTablas.sql -C && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -d SAMS.Data -i /usr/src/app/scriptObjetos.sql -C
