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

Para poder ejecutar el sistema correctamente, es necesario configurar la conexión a la base de datos. Para ello, debes crear un archivo `appsettings.json` en las carpetas raíz de los proyectos **SAMS.UI** y **SAMS.Test**.

### Estructura de `appsettings.json`

```json
{
  "ConnectionStrings": {
    "SAMSDatabase": "Server=IP_HOST;Database=BASE_DATOS;User ID=USUARIO;Password=CONTRASEÑA;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

Reemplaza los siguientes valores con la información adecuada:

- **IP_HOST**: La dirección IP del servidor de la base de datos.
- **BASE_DATOS**: El nombre de la base de datos.
- **USUARIO**: El usuario de acceso a la base de datos.
- **CONTRASEÑA**: La contraseña del usuario.

El archivo `appsettings.json` debe estar presente en el directorio raíz de ambos proyectos (**SAMS.UI** y **SAMS.Test**) para que el sistema pueda establecer correctamente la conexión a la base de datos.

## 🛠️ Tecnologías Utilizadas

- **.NET 8**: El proyecto está basado en la última versión de .NET, proporcionando mejoras en el rendimiento y nuevas características para el desarrollo.
- **WPF (Windows Presentation Foundation)**: Framework para la creación de aplicaciones de escritorio con interfaz gráfica.
- **Entity Framework**: ORM utilizado para interactuar con la base de datos de forma sencilla y eficiente.
- **Microsoft SQL Server**: Base de datos relacional para almacenar la información del sistema.
- **XUnit**: Framework de pruebas unitarias utilizado en el proyecto **SAMS.Test** para asegurar la calidad del código y realizar pruebas automatizadas.

## 🧑‍💻 Contribuir

Si deseas contribuir al proyecto, por favor sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una rama con tu nueva funcionalidad (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Añadir nueva funcionalidad'`).
4. Empuja tus cambios a tu fork (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request con una descripción detallada de tus cambios.