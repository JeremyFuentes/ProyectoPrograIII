# 📦 EasyCommerce - Frontend

Este repositorio contiene la interfaz de usuario del sistema EasyCommerce, un sistema de gestión de productos, usuarios y favoritos con autenticación integrada, desarrollado como proyecto de cátedra.

## 🖼️ Tecnologías utilizadas

- **HTML5**
- **CSS3**
- **JavaScript (Vanilla)**
- **Bootstrap 5.3**
- **FontAwesome**
- **Fetch API**
- **LocalStorage**

## Explicacion de la estructura 

El proyecto fronted se a creado en un sistema de carpetas que almacena los archivos css, html y js de forma separada. Cada sistema de carpetas se divide en mas subcarpetas como por ejemplo la carpeta html almacena la carpeta de usuarios y admin las cuales alacenas las paginas principales para estos roles asi como la carpeta js esta diseñada para manejar de manera individual cada pagina con las que cuenta nuestro proyecto a continuacion se presenta un esquema de como esta estructurado.

## 🚀 Estructura del proyecto

```
/frontend
│
│──assets/
│   ├──icons/  #Se almacenan algunos iconos utilizados en el desarrollo 
│   └──img/    # Aca se almacenas las imagenes pertenecientes a la pagina
│
├── css/
│   ├── styleindexadmin.css    # Estilos específicos del administrador
│   ├── stylelogin.css         # Estilos para el login de usuario
│   └── style.css              # Estilos generales
│
├── html/                         # Contenedor de todas las vistas html
│   ├──├─ Admin/                  # Contemedor de las vistas de administrador
│   │  ├── indexadmin.html        # Vista principal del administrador
│   │  ├── loginadmin.html        # Login de administrador
│   │  └── agregarproductos.html  # Agregar productos y editar productos del administrador
│   │
│   ├──├─ Usuario/
│   │  ├── carritousuario.html      # Vista del carrito de compras del usuario
│   │  ├── favoritousuario.html     # Vista de los producto favoritos del usuario
│   │  ├── indexusuario.html        # Vista principal del usuario
│   │  └── productosusuario.html    # Vista de productos del usuario
│   │
│   ├──index.html           # Vista principal de usuario sin loguear
│   ├──login.html           # Login del usuario
│   └──productos.html       # Vista [productos sin loguear]
│
└── js/                             # Scripts JavaScript
    ├──├─ usuarios/                 # Contemedor de los controladores del usuario
    │  ├── carritousuarios.html     # Controlador para e carrito del usuario
    │  ├── favoritosusuario.html    # Controlador de favoritos del usuario
    │  ├── indexusuario.html        # Controlador de la vista principal del usuario
    │  └── productosusuario.html    # Controlador de la vista de productos del usuario
    │
    ├── adminlogincontroller.js         # Controllador del login del administrador
    ├── adminprodcutoscontroller.js     # Controladot de los productos mostrados al admin
    ├── agregarproductos.js             # Controlador para agregar y actualizar productos
    ├── controllerlogin.js              # Controlador del login del usuario
    ├── productos.js                    # Controlador de carga de los productos en usuario
    └── script.js                       # Script general del proyecto
    
```
## 🔐 Autenticación

Se incluye autenticación local y autenticación con Google. El botón de autenticación por Google está implementado mediante OAuth2 y verificado desde backend con token.

> 🔎 *Se utiliza una versión más antigua de la librería de autenticación de Google por razones de compatibilidad con el entorno del proyecto.*

---

## 📄 Principales funcionalidades

| Página | Funcionalidad Destacada |
|--------|-------------------------|
| `login.html` | Registro y login de usuarios locales y con Google |
| `Admin/indexadmin.html` | Vista del administrador con productos y control |
| `Admin/agregarproductos.html` | Crear/editar productos con galería de imágenes |
| `usuarios/indexusuario.html` | Ver catálogo de productos y añadir a favoritos |
| `usuarios/favoritosusuario.html` | Mostrar productos favoritos por usuario |
| `usuarios/carrito.html` | Gestión del carrito de compras (próximamente) |

---

## 📋 Instalación y ejecución

1. Clonar el repositorio del backend y frontend en carpetas separadas.
2. Ejecutar el backend (`WebApi_Easy_Commerce`) con Visual Studio o desde consola.
3. Abrir `login.html` en un navegador moderno.
4. Registrar un usuario o ingresar con Google.
5. Comenzar a navegar la plataforma.

---