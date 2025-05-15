USE [master]
GO
/****** Object:  Database [ProyectoProgra3]    Script Date: 14/5/2025 15:14:03 ******/
CREATE DATABASE [ProyectoProgra3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoProgra3', FILENAME = N'C:\SQLData\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoProgra3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoProgra3_log', FILENAME = N'C:\SQLData\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoProgra3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProyectoProgra3] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoProgra3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoProgra3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProyectoProgra3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoProgra3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoProgra3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoProgra3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoProgra3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoProgra3] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoProgra3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoProgra3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoProgra3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoProgra3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoProgra3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoProgra3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProyectoProgra3] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoProgra3] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoProgra3]
GO
/****** Object:  Table [dbo].[Administradores]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administradores](
	[IdAdmin] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Correo] [nvarchar](150) NULL,
	[ContrasenaHash] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[CarritoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [float] NULL,
	[FechaCompra] [datetime] NULL,
	[EstadoProductoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CarritoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[CategoriaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[ComentarioID] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NULL,
	[ServicioID] [int] NULL,
	[UsuarioID] [int] NULL,
	[Comentario] [nvarchar](255) NULL,
	[Calificacion] [int] NULL,
	[Fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ComentarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DetalleID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NULL,
	[ProductoID] [int] NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [float] NULL,
	[Subtotal] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[DetalleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[EmpleadoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Direccion] [nvarchar](150) NULL,
	[Contacto] [nvarchar](20) NULL,
	[Correo] [nvarchar](50) NULL,
	[Edad] [int] NULL,
	[DUI] [nvarchar](10) NULL,
	[Horario] [nvarchar](50) NULL,
	[Cargo] [nvarchar](50) NULL,
	[Contraseña] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpleadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadosProducto]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadosProducto](
	[EstadoProductoId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturacion]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturacion](
	[FacturaID] [int] IDENTITY(1,1) NOT NULL,
	[EmpleadoID] [int] NULL,
	[UsuarioID] [int] NULL,
	[Fecha] [datetime] NULL,
	[Local] [nvarchar](50) NULL,
	[Total] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[FacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favoritos]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favoritos](
	[FavoritoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NULL,
	[ProductoID] [int] NULL,
	[ServicioID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FavoritoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImagenesProducto]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImagenesProducto](
	[IdImagen] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NULL,
	[UrlImagen] [nvarchar](500) NULL,
	[Descripcion] [nvarchar](255) NULL,
	[EsPrincipal] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[MarcaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MarcaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Precio] [float] NULL,
	[Stock] [int] NULL,
	[CategoriaID] [int] NULL,
	[SKU] [nvarchar](20) NULL,
	[Descripcion] [nvarchar](150) NULL,
	[MarcaID] [int] NULL,
	[ProveedorID] [int] NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promociones]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promociones](
	[PromocionID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](200) NULL,
	[FechaInicio] [datetime] NULL,
	[FechaFin] [datetime] NULL,
	[ProductoID] [int] NULL,
	[ServicioID] [int] NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PromocionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[ProveedorID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Contacto] [nvarchar](20) NULL,
	[Correo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProveedorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[ServicioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](200) NULL,
	[Precio] [float] NULL,
	[CategoriaID] [int] NULL,
	[Estado] [bit] NULL,
	[Materiales] [nvarchar](200) NULL,
	[AreaServicio] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ServicioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 14/5/2025 15:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Direccion] [nvarchar](150) NULL,
	[Contacto] [nvarchar](20) NULL,
	[Correo] [nvarchar](50) NULL,
	[GoogleID] [nvarchar](50) NULL,
	[Contraseña] [varchar](255) NULL,
	[MetodoLogin] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Administradores] ON 
GO
INSERT [dbo].[Administradores] ([IdAdmin], [Nombre], [Correo], [ContrasenaHash]) VALUES (1, N'', N'', N'')
GO
INSERT [dbo].[Administradores] ([IdAdmin], [Nombre], [Correo], [ContrasenaHash]) VALUES (2, N'Jeremy Fuentes', N'adminjeremy@easycommerce.com', N'AQAAAAIAAYagAAAAEPizHrCEcaBggWeWR4YMHaK8zFcArjVAspcJ0nn93YpyfEh89RIMZUiS4S3MTBg1vQ==')
GO
SET IDENTITY_INSERT [dbo].[Administradores] OFF
GO
SET IDENTITY_INSERT [dbo].[Carrito] ON 
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (4, 1, 1, 1, 1100, CAST(N'2025-05-09T00:37:06.267' AS DateTime), 5)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (6, 1, 1, 1, 1100, CAST(N'2025-05-09T10:31:51.633' AS DateTime), 4)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (7, 1, 1, 1, 1100, NULL, 1)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (8, 1, 11, 1, 129, NULL, 1)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (9, 1, 14, 1, 159, NULL, 1)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (10, 2, 3, 1, 1099, CAST(N'2025-05-14T14:54:46.347' AS DateTime), 2)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (11, 2, 14, 1, 159, CAST(N'2025-05-14T14:54:46.347' AS DateTime), 2)
GO
INSERT [dbo].[Carrito] ([CarritoID], [UsuarioID], [ProductoID], [Cantidad], [PrecioUnitario], [FechaCompra], [EstadoProductoId]) VALUES (12, 2, 19, 1, 149, CAST(N'2025-05-14T14:55:04.577' AS DateTime), 5)
GO
SET IDENTITY_INSERT [dbo].[Carrito] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (1, N'Laptops', N'Laptops de disferentes marcas y estilos')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (2, N'Tarjetas Graficas', N'Tarjetas grafias de Nvidia, AMD e Intel')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (3, N' Motherboards
', N'Tarjetas madre de computadoras de escritorio')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (4, N'Memoria RAM
', N'Memorias DDR4 y DDR5 para pc y laptop')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (5, N'Procesadores', N'Procesadores Intel y AMD')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (6, N'Almacenamiento
', N'Discos Duros y Solidos')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (7, N' Periféricos', N'Mause, teclados microfonos y mas')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (8, N'Enfriamiento', N'Enfriamiento por aire y liquidas todo en uno ')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (9, N'Gabinetes
', N'Gabinetes de diferentes tamaños y estilos')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (10, N'Fuentes de poder', N'Fuentes de poder de distintos watts')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (11, N'Redes
', N'Router, switch y cables y otros')
GO
INSERT [dbo].[Categoria] ([CategoriaID], [Nombre], [Descripcion]) VALUES (12, N'Accesorios
', N'Accesorios adicionales para tu setup')
GO
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadosProducto] ON 
GO
INSERT [dbo].[EstadosProducto] ([EstadoProductoId], [NombreEstado]) VALUES (1, N'Pendiente de Pago')
GO
INSERT [dbo].[EstadosProducto] ([EstadoProductoId], [NombreEstado]) VALUES (2, N'En transaccion')
GO
INSERT [dbo].[EstadosProducto] ([EstadoProductoId], [NombreEstado]) VALUES (3, N'Enviado')
GO
INSERT [dbo].[EstadosProducto] ([EstadoProductoId], [NombreEstado]) VALUES (4, N'En Reparto')
GO
INSERT [dbo].[EstadosProducto] ([EstadoProductoId], [NombreEstado]) VALUES (5, N'Entregado')
GO
SET IDENTITY_INSERT [dbo].[EstadosProducto] OFF
GO
SET IDENTITY_INSERT [dbo].[Favoritos] ON 
GO
INSERT [dbo].[Favoritos] ([FavoritoID], [UsuarioID], [ProductoID], [ServicioID]) VALUES (1, 1, 1, NULL)
GO
INSERT [dbo].[Favoritos] ([FavoritoID], [UsuarioID], [ProductoID], [ServicioID]) VALUES (2, 1, 5, NULL)
GO
INSERT [dbo].[Favoritos] ([FavoritoID], [UsuarioID], [ProductoID], [ServicioID]) VALUES (3, 1, 9, NULL)
GO
SET IDENTITY_INSERT [dbo].[Favoritos] OFF
GO
SET IDENTITY_INSERT [dbo].[ImagenesProducto] ON 
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (1, 1, N'/imagenes/4dc12b56-b2a0-4f50-9eca-837aa4decba1_ASUS TUF Gaming F15.png', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (3, 2, N'/imagenes/59350972-b300-45df-90a5-bd74615d35df_Dell XPS 13.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (5, 3, N'/imagenes/844c810b-19cc-4276-9873-5bebd03580d6_MacBook Air M2 1.jpg', NULL, 0)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (8, 3, N'/imagenes/269b406d-72fc-46f8-aa11-ff424bbd7470_MacBook Air M2 2.webp', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (11, 4, N'/imagenes/39888858-1c58-4c3d-bf3e-de1631abcd8c_images.jpeg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (12, 5, N'/imagenes/9244bbd0-bc87-49eb-9d68-0b4062cdfe15_Gigabyte GeForce RTX 4070 Ti.webp', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (14, 6, N'/imagenes/60b22d24-56fc-43f3-8153-4f1c71adbb48_ASUS ROG Strix B550-F.png', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (18, 7, N'/imagenes/40db8745-3e80-4bf5-8f34-f14a74d33530_LD0005773909_1_0005774245.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (19, 8, N'/imagenes/4be44b8c-e38c-4db6-9cb8-1fff2153da04_Kingston Fury Beast 16GB.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (21, 9, N'/imagenes/33f96a55-e947-4361-a424-d5bcc358ab98_Intel Core i9-13900K.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (23, 10, N'/imagenes/37a932ba-ec29-46b9-96b5-7cec7a8a2b62_imagen_generada67f3ebc6e1c40.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (25, 11, N'/imagenes/b164fdee-3e75-4eb5-a7a6-99d25f9ad7e4_61KeSQhDm4L._AC_UF1000,1000_QL80_.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (27, 12, N'/imagenes/a6426c77-acde-491b-9136-3aaaeb98cf0d_Seagate IronWolf 4TB.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (28, 12, N'/imagenes/3e542645-741a-4281-8ecb-2728b595ca81_Seagate IronWolf 4TB.jpg', NULL, 0)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (29, 13, N'/imagenes/a405dc67-39fb-484d-a785-eeaa9ec33b2d_71zhBR0KB2L.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (31, 13, N'/imagenes/eed3ee91-c77f-4f42-835a-0095338b6ac8_61JkTXrgYxS._AC_SL1500_.jpg', NULL, 0)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (32, 14, N'/imagenes/6c2d4e71-4202-4e6e-9fb6-09096736cf31_Logitech G Pro X Superlight.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (34, 15, N'/imagenes/6a5a1735-4aed-4b74-a2e7-13da97fd7872_Razer BlackWidow V4 2.webp', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (37, 15, N'/imagenes/f6e32366-2b82-4ee0-b47c-13ee229ee454_Razer BlackWidow V4.webp', NULL, 0)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (38, 16, N'/imagenes/f08b695d-6726-4180-81bf-d9a8cc87ce8c_Cooler Master Hyper 212 Black Edition 2.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (41, 16, N'/imagenes/048b6862-2134-435f-8a66-1c313a2c7873_Cooler Master Hyper 212 Black Edition.jpg', NULL, 0)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (42, 17, N'/imagenes/162be4ff-c712-4fae-8cb8-f6814d8ec5bd_NZXT H510 Elite.webp', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (44, 18, N'/imagenes/b41ece49-adcc-415a-a934-3e0a16bd7d85_EVGA 750W GQ.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (46, 19, N'/imagenes/d4cc2420-fb0c-48a8-abc8-22e5dc2c76c7_TP-Link Archer AX73.jpg', NULL, 1)
GO
INSERT [dbo].[ImagenesProducto] ([IdImagen], [ProductoID], [UrlImagen], [Descripcion], [EsPrincipal]) VALUES (48, 20, N'/imagenes/5416ae3f-b9b3-4618-bb76-76ff509eae2d_Elgato Stream Deck MK.2.jpg', NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ImagenesProducto] OFF
GO
SET IDENTITY_INSERT [dbo].[Marca] ON 
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (1, N'ASUS
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (2, N'Dell
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (3, N'Apple')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (4, N'Lenovo
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (5, N'Gigabyte
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (6, N'Corsair')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (7, N'Kingston')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (8, N'Intel')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (9, N'AMD
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (10, N'Western Digital
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (11, N'Seagate
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (12, N'Samsung
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (13, N'Logitech')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (14, N'Razer')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (15, N'Cooler Master')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (16, N'NZXT')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (17, N'EVGA')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (18, N'TP-Link
')
GO
INSERT [dbo].[Marca] ([MarcaID], [Nombre]) VALUES (19, N'Elgato')
GO
SET IDENTITY_INSERT [dbo].[Marca] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (1, N'ASUS TUF Gaming F15', 1100, 16, 1, N'PRD-20250508-001', N': Laptop gamer con procesador Intel Core i7, 16GB RAM, 512GB SSD y tarjeta gráfica RTX 3050', 1, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (2, N'Dell XPS 13', 1250, 30, 1, N'PRD-20250508-002', N' Ultrabook con pantalla 13.4" FHD+, Intel Core i5, 16GB RAM y 512GB SSD', 2, 3, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (3, N' MacBook Air M2', 1099, 39, 1, N'PRD-20250508-003', N' Laptop ultraligera con chip M2, 8GB RAM, 256GB SSD y pantalla Retina de 13"', 3, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (4, N'Lenovo Legion 5', 1399, 12, 1, N'PRD-20250508-004', N' Laptop gamer con AMD Ryzen 7, 16GB RAM, 1TB SSD y RTX 4060. ● Marca: Lenovo', 4, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (5, N'Gigabyte GeForce RTX 4070 Ti', 799, 22, 2, N'PRD-20250508-005', N'Tarjeta gráfica con 12GB GDDR6X, perfecta para juegos exigentes y diseño.', 5, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (6, N'ASUS ROG Strix B550-F', 189, 50, 3, N'PRD-20250508-006', N'Placa madre para AMD con soporte para Ryzen 5000, PCIe 4.0 y RGB.', 1, 2, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (7, N'Corsair Vengeance RGB Pro 32GB', 145, 70, 4, N'PRD-20250508-007', N'Memoria RAM DDR4 (2x16GB) 3600MHz con RGB sincronizable.', 6, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (8, N'Kingston Fury Beast 16GB', 65, 90, 4, N'PRD-20250508-008', N'Módulo de RAM DDR4 3200MHz ideal para gaming y productividad', 7, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (9, N'Intel Core i9-13900K', 589, 23, 5, N'PRD-20250508-009', N' Procesador de 13ª generación con 24 núcleos y 32 hilos para tareas intensivas.', 8, 4, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (10, N'AMD Ryzen 7 7800X3D', 459, 31, 5, N'PRD-20250508-010', N'CPU de alto rendimiento para gaming y edición, con 8 núcleos y 3D V-Cache', 9, 4, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (11, N'WD Black SN850X 1TB', 129, 44, 6, N'PRD-20250508-011', N'SSD NVMe PCIe Gen4 con velocidades de lectura de hasta 7300MB/s', 10, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (12, N'Seagate IronWolf 4TB', 120, 13, 6, N'PRD-20250508-012', N'Disco duro diseñado para NAS, 7200rpm, optimizado para uso 24/7.', 11, 6, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (13, N'Samsung 980 Pro 2TB', 199, 7, 6, N'PRD-20250508-013', N'SSD NVMe PCIe Gen4 con velocidades ultra rápidas para gaming y edición.', 12, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (14, N'Logitech G Pro X Superlight', 159, 25, 7, N'PRD-20250508-014', N' Mouse gamer inalámbrico ultraligero con sensor HERO de alto rendimiento.', 13, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (15, N'Razer BlackWidow V4', 179, 20, 7, N'PRD-20250508-015', N'Teclado mecánico RGB con switches verdes para respuesta táctil precisa.', 14, 3, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (16, N' Cooler Master Hyper 212 Black Edition', 45, 62, 8, N'PRD-20250508-016', N'Disipador de aire para CPU con ventilador silencioso y diseño elegante', 15, 2, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (17, N' NZXT H510 Elite', 149, 35, 9, N'PRD-20250508-017', N'Gabinete mid-tower con vidrio templado y excelente flujo de aire', 16, 5, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (18, N'EVGA 750W GQ', 109, 40, 10, N'PRD-20250508-018', N'Fuente de poder 80+ Gold semi-modular, ideal para PCs de alto rendimiento', 17, 1, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (19, N'TP-Link Archer AX73', 149, 16, 11, N'PRD-20250508-019', N'Router Wi-Fi 6 de alto rendimiento con 6 antenas y velocidades de hasta 5400 Mbps.', 18, 7, 1)
GO
INSERT [dbo].[Productos] ([ProductoID], [Nombre], [Precio], [Stock], [CategoriaID], [SKU], [Descripcion], [MarcaID], [ProveedorID], [Estado]) VALUES (20, N'Elgato Stream Deck MK.2', 149, 50, 12, N'PRD-20250509-001', N'Controlador de 15 teclas LCD programables para streamers y creadores', 19, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedores] ON 
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (1, N'TechZone S.A.', N'78412516', N'TechZone@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (2, N'CompuRed Distribuciones', N'75894216', N'CompuRedDistribuciones@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (3, N'Grupo ElectroHard', N'582614921', N'GrupoElectroHard@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (4, N'IT Global Supplies', N'310220602', N'ITGlobalSupplies@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (5, N'MegaTech Importaciones', N'172827542', N'MegaTechImportaciones@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (6, N'Distribuidora BytePlus', N'272572572', N'DistribuidoraBytePlus@gmail.com')
GO
INSERT [dbo].[Proveedores] ([ProveedorID], [Nombre], [Contacto], [Correo]) VALUES (7, N'PC Supply El Salvador', N'58963124', N'pcsupply@elsalvador.com')
GO
SET IDENTITY_INSERT [dbo].[Proveedores] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [Direccion], [Contacto], [Correo], [GoogleID], [Contraseña], [MetodoLogin]) VALUES (1, N'Jeremy Fuentes', N'Concepcion Quezaltepeque', N'78583590', N'jeremyfuentes201@gmail.com', N'106001482557184685387', NULL, N'Google')
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [Direccion], [Contacto], [Correo], [GoogleID], [Contraseña], [MetodoLogin]) VALUES (2, N'Karla Miranda', N'Chalatenango', N'74953681', N'karla@gmail.com', NULL, N'AQAAAAIAAYagAAAAEJytKAulgl4DY9dkW0o9Xbg6W94jZ6SR1QxQ+POdB4a+gflZjDeCcKjwL+9Yzww60Q==', N'Propio')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Administ__60695A1993AAEC53]    Script Date: 14/5/2025 15:14:04 ******/
ALTER TABLE [dbo].[Administradores] ADD UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Correo]    Script Date: 14/5/2025 15:14:04 ******/
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [UQ_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carrito] ADD  DEFAULT ((1)) FOR [EstadoProductoId]
GO
ALTER TABLE [dbo].[Comentarios] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Facturacion] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[ImagenesProducto] ADD  DEFAULT ((0)) FOR [EsPrincipal]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ('Propio') FOR [MetodoLogin]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_EstadoProducto] FOREIGN KEY([EstadoProductoId])
REFERENCES [dbo].[EstadosProducto] ([EstadoProductoId])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_EstadoProducto]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Producto] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Producto]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Usuario] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Usuario]
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD FOREIGN KEY([ServicioID])
REFERENCES [dbo].[Servicios] ([ServicioID])
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([FacturaID])
REFERENCES [dbo].[Facturacion] ([FacturaID])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Facturacion]  WITH CHECK ADD FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([EmpleadoID])
GO
ALTER TABLE [dbo].[Facturacion]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Favoritos]  WITH NOCHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Favoritos]  WITH NOCHECK ADD FOREIGN KEY([ServicioID])
REFERENCES [dbo].[Servicios] ([ServicioID])
GO
ALTER TABLE [dbo].[Favoritos]  WITH NOCHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[ImagenesProducto]  WITH NOCHECK ADD  CONSTRAINT [FK_ImagenesProducto_Productos] FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImagenesProducto] CHECK CONSTRAINT [FK_ImagenesProducto_Productos]
GO
ALTER TABLE [dbo].[Productos]  WITH NOCHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([CategoriaID])
GO
ALTER TABLE [dbo].[Productos]  WITH NOCHECK ADD FOREIGN KEY([MarcaID])
REFERENCES [dbo].[Marca] ([MarcaID])
GO
ALTER TABLE [dbo].[Productos]  WITH NOCHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
GO
ALTER TABLE [dbo].[Promociones]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Promociones]  WITH CHECK ADD FOREIGN KEY([ServicioID])
REFERENCES [dbo].[Servicios] ([ServicioID])
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([CategoriaID])
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD CHECK  (([Calificacion]>=(1) AND [Calificacion]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [ProyectoProgra3] SET  READ_WRITE 
GO
