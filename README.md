# 🛒 Sistema de Administración para Supermercados

Este repositorio contiene el código fuente del sistema de administración para una cadena de supermercados, diseñado para optimizar la gestión de productos, pedidos, inventarios, proveedores, usuarios, finanzas, y mejorar el flujo de caja y la logística interna.

## 📜 Descripción del Proyecto

Este sistema tiene como objetivo automatizar las operaciones de la cadena de supermercados, reduciendo errores y mejorando la eficiencia operativa. Utiliza una arquitectura cliente-servidor con **WPF** (Windows Presentation Foundation) para la interfaz de usuario y **Microsoft SQL Server** como base de datos.

## 👩‍💻👨‍💻 Contribuidores
<a href="https://github.com/alcrivico/SAMS/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=alcrivico/SAMS" />
</a>

## 🚀 Objetivo

Automatizar las operaciones del supermercado, optimizando la gestión de inventarios, el procesamiento de pedidos, y la administración financiera, reduciendo tiempos de respuesta y errores humanos.

## ⚙️ Configuración

> [!IMPORTANT]  
> Para poder ejecutar el sistema correctamente, es necesario configurar la conexión a la base de datos. Para ello, debes crear un archivo `appsettings.json` en las carpetas raíz de los proyectos **SAMS.UI** y **SAMS.Test**.
> 
> ### Estructura de `appsettings.json`
> 
> ```json
> {
>   "ConnectionStrings": {
>     "SAMSDatabase": "Server=IP_HOST;Database=BASE_DATOS;User ID=USUARIO;Password=CONTRASEÑA;TrustServerCertificate=True;Encrypt=False;"
>   }
> }
> ```
> 
> Reemplaza los siguientes valores con la información adecuada:
> 
> - **IP_HOST**: La dirección IP del servidor de la base de datos.
> - **BASE_DATOS**: El nombre de la base de datos.
> - **USUARIO**: El usuario de acceso a la base de datos.
> - **CONTRASEÑA**: La contraseña del usuario.

> [!WARNING] 
> El archivo `appsettings.json` debe estar presente en el directorio raíz de ambos proyectos (**SAMS.UI** y **SAMS.Test**) para que el sistema pueda establecer correctamente la conexión a la base de datos.

## 🗃️ Migraciones de la Base de Datos

Para poder gestionar cambios en la estructura de la base de datos, este proyecto utiliza Entity Framework Core. A continuación, se detallan los pasos necesarios para crear y aplicar migraciones.

### 🛠️ Crear una Nueva Migración

Si realizas cambios en las clases de tus modelos (o entidades), deberás crear una nueva migración para que los cambios se reflejen en la base de datos.

Ejecuta el siguiente comando en la Consola del Administrador de Paquetes:

```powershell
add-migration NOMBRE_MIGRACION -p SAMS.UI -c SAMSContext -o Models/DataContext/Migrations -s SAMS.UI
```
- NOMBRE_MIGRACION: Reemplázalo con un nombre descriptivo para tu migración (por ejemplo, AddCategorias).
- **-p SAMS.UI**: Especifica el proyecto que contiene el contexto (SAMSContext).
- **-c SAMSContext**: Indica la clase de contexto de la base de datos que se utilizará.
- **-o Models/DataContext/Migrations**: Define la ruta donde se generará la migración.
- **-s SAMS.UI**: Establece el proyecto de inicio (startup project).

### 📦 Aplicar los Cambios a la Base de Datos

Después de crear la migración, necesitas actualizar la base de datos para aplicar los cambios:

```powershell
Update-Database -p SAMS.UI -s SAMS.UI
```

### *⚠️ Notas Importantes*

> [!IMPORTANT]  
>1. Asegúrate de que el archivo appsettings.json está correctamente configurado en la carpeta raíz de SAMS.UI antes de ejecutar los comandos de migración.
>2. Ejecuta siempre Update-Database después de crear una migración para que los cambios se reflejen en la base de datos.
>. Si necesitas revertir una migración, puedes usar:
>
>```powershell
>Remove-Migration -p SAMS.UI -s SAMS.UI
>```

## 🚀 Configuración del Contenedor Docker
Este proyecto utiliza Docker y Docker Compose para gestionar el entorno de base de datos SQL Server. Sigue los siguientes pasos para configurar y ejecutar el contenedor Docker para el sistema.

### 🛠️ Requisitos Previos
Antes de comenzar, asegúrate de tener las siguientes herramientas instaladas en tu máquina:

Docker: Guía de instalación
Docker Compose: Guía de instalación
### 🧑‍💻 Comandos de Docker
>### Levantar el Contenedor
>Para construir y ejecutar el contenedor por primera vez o >después de realizar cambios en los archivos, ejecuta:

```bash
docker-compose up --build
```

>Este comando:
> - Construye la imagen del contenedor, asegurándose de que todos los archivos, como los scripts SQL y la configuración, se copien correctamente.
> - Levanta el contenedor y lo pone en funcionamiento con los scripts SQL necesarios para inicializar la base de datos.

>### 2. Detener y Eliminar el Contenedor
>Si necesitas detener y eliminar el contenedor, puedes usar:

```bash
docker-compose down
```

> Este comando:
> - Detiene el contenedor en ejecución.
> - Elimina los contenedores, redes y volúmenes definidos en el archivo docker-compose.yml.

### 📝 Archivos y Estructura del Contenedor
El contenedor utiliza un archivo Dockerfile que define la construcción de la imagen con SQL Server, incluyendo la instalación de dependencias y la ejecución de los scripts SQL al iniciar el contenedor. Asegúrate de tener los archivos SQL (dbo.sql, scriptDatos.sql, scriptTablas.sql, scriptObjetos.sql) en el directorio raíz de tu proyecto para que se copien correctamente al contenedor.
Detiene el contenedor en ejecución.
Elimina los contenedores, redes y volúmenes definidos en el archivo docker-compose.yml.

> [!IMPORTANT]
> Por defecto se crea un a contraseña en sa llamada StrongPassword123!, de preferencia crear otro usuario y cambiar la contraseña sa

## 🛠️ Tecnologías Utilizadas

- **.NET 8**.
- **WPF (Windows Presentation Foundation)**.
- **Entity Framework**.
- **Microsoft SQL Server**.
- **XUnit**: Framework de pruebas unitarias utilizado en el proyecto **SAMS.Test**.

## 🧑‍💻 Contribuir

Si deseas contribuir al proyecto, por favor sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una rama con tu nueva funcionalidad (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Añadir nueva funcionalidad'`).
4. Empuja tus cambios a tu fork (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request con una descripción detallada de tus cambios.