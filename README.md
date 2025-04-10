
# 🛍️ Easy Commerce API - Proyecto de Cátedra

Este proyecto representa la API desarrollada para el sistema de comercio electrónico **Easy Commerce**, construida con ASP.NET Core y Entity Framework Core. Incluye autenticación de usuarios, administración de productos, manejo de imágenes, y favoritos.

---

## 📦 Paquetes Requeridos

Antes de compilar el proyecto asegúrate de tener instaladas las siguientes dependencias NuGet:

| Paquete | Versión | Descripción |
|--------|---------|-------------|
| `Google.Apis.Auth` | 1.69.0 | Cliente para autenticación de Google |
| `Microsoft.AspNetCore.Authentication.Google` | 8.0.14 | Middleware de autenticación con Google |
| `Microsoft.AspNetCore.Identity` | 2.3.1 | Gestión de usuarios y contraseñas |
| `Microsoft.EntityFrameworkCore` | 9.0.3 | ORM para trabajar con base de datos relacional |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.3 | Herramientas para diseño de base de datos |
| `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.3 | Provider para SQL Server |
| `Microsoft.EntityFrameworkCore.Tools` | 9.0.3 | Herramientas para migraciones |
| `Swashbuckle.AspNetCore` | 6.6.2 / 8.1.0 | Swagger para documentación de la API |

---

## ⚠️ Compatibilidad con Google Auth

> Este proyecto utiliza la versión **8.0.14** del paquete `Microsoft.AspNetCore.Authentication.Google` por motivos de compatibilidad con ASP.NET Core 8 y la configuración general del entorno de desarrollo.  
> Si estás trabajando con una versión más reciente de ASP.NET, asegúrate de revisar los cambios en las políticas de autenticación OAuth.


## 📁 Estructura del Proyecto
```
📁 Solución "TiendaElectronica"
/ProyectoPrograIII
│
├── Context/
│   └── ProyectoProgra3Context.cs (contexto de Entity Framework)
│
├── Models/
│   └── Clases de entidades (Usuario, Producto, ImagenesProducto, Favorito, Administrador, etc.)
│
├── Repository/
│   └── DAOs (LoginDao, ProductoDao, FavoritoDao, ImagenesProductoDao, etc.)
│
└── Class1.cs (archivo base o auxiliar)

/WebApi_Easy_Commerce
│
├── wwwroot/
│   └── imagenes/ (almacenamiento estático de imágenes de productos)
│
├── Controllers/
│   ├── ProductosController.cs
│   ├── LoginController.cs
│   ├── ImagenesProductoController.cs
│   ├── FavoritosController.cs
│   ├── AuxiliarController.cs
│   └── AdministradorController.cs
│
├── appsettings.json (configuración general del proyecto)
├── Program.cs (punto de entrada de la aplicación)
└── WebApi_Easy_Commerce.http (archivo de pruebas para endpoints HTTP)
```

## 🔑 Endpoints Clave

| Endpoint | Método | Descripción |
|---------|--------|-------------|
| `/usuarios/CrearUsuario` | POST | Registrar usuario (con contraseña hasheada) |
| `/usuarios/AutenticarUsuario` | POST | Login con verificación de hash |
| `/usuarios/AutenticarGoogle` | POST | Login con Google usando ID Token |
| `/productos/RegistrarConImagenes` | POST | Registrar producto con imágenes adjuntas |
| `/productos/conImagenPrincipal` | GET | Obtener productos con su imagen principal |
| `/imagenesProducto/subirVarias` | POST | Subir imágenes adicionales (modo edición) |
| `/favoritos/ConProductoPorUsuario/{id}` | GET | Obtener favoritos de un usuario con producto |
| `/admin/login` | POST | Login de administrador |
| `/auxiliares/categorias/todas` | GET | Obtener categorías |
| `/auxiliares/marcas/todas` | GET | Obtener marcas |
| `/auxiliares/proveedores/todos` | GET | Obtener proveedores |

---

## 🚀 Cómo iniciar el proyecto

1. Clonar el repositorio.
2. MOdifica la cadena de conexion en el contexto 
3. Ejecutar migraciones de Entity Framework si es necesario.
4. Iniciar la API (`WebApi_Easy_Commerce`) como proyecto principal.
5. Consumir desde el frontend con rutas hacia `https://localhost:7291`.

---

## 🧪 Swagger

Accede a la documentación interactiva desde:  
`https://localhost:7291/swagger`

---

## 📌 Notas Finales

- Las contraseñas se almacenan utilizando `PasswordHasher` para garantizar seguridad.
- El frontend puede trabajar con rutas relativas `/imagenes/{nombreArchivo}` para mostrar imágenes.

---

## ✅ Recomendaciones

- Asegúrate de tener creada la base de datos antes de ejecutar la API.
- El servidor espera archivos en `wwwroot/imagenes`, asegúrate de que exista.
- Usa [Postman](https://www.postman.com/) o el archivo `WebApi_Easy_Commerce.http` para probar la API localmente.

---

_Proyecto desarrollado como parte de Catedra de Programacion 3
