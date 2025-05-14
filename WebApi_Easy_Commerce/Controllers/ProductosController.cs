using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProyectoProgra3Context _contexto;
        private readonly ProductoDao _dao;

        public ProductosController(ProyectoProgra3Context contexto)
        {
            _contexto = contexto;
            _dao = new ProductoDao(contexto); // le pasaremos el contexto
        }

        #region Obtener todos los productos
        [HttpGet("ObtenerTodos")]
        public IActionResult GetAll() => Ok(_dao.GetAll());
        #endregion

        #region Obtener producto por ID
        [HttpGet("ObtenerporId/{id}")]
        public IActionResult GetById(int id)
        {
            var producto = _dao.GetById(id);
            return producto != null ? Ok(producto) : NotFound();
        }
        #endregion

        #region Obtener productos por MarcaID
        [HttpGet("marca/{marcaId}")]
        public IActionResult GetByMarca(int marcaId)
        {
            return Ok(_dao.GetByMarca(marcaId));
        }
        #endregion

        #region Obtener productos favoritos por UsuarioID
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetByUsuario(int usuarioId)
        {
            return Ok(_dao.GetByUsuario(usuarioId));
        }
        #endregion

        #region Obtener productos por Nombre
        [HttpGet("nombre/{nombre}")]
        public IActionResult GetByNombre(string nombre)
        {
            return Ok(_dao.GetByNombre(nombre));
        }
        #endregion

        #region Insertar producto
        [HttpPost("InsertarProducto")]
        public IActionResult Insertar([FromBody] Producto producto)
        {
            return _dao.Insertar(producto) ? Ok("Producto insertado") : BadRequest("Error al insertar");
        }
        #endregion

        #region InsertarProdcutoConImagen
        [HttpPost("RegistrarConImagenes")]
        public async Task<IActionResult> RegistrarConImagenes([FromForm] Producto producto, List<IFormFile> imagenes)
        {
            if (producto == null || imagenes == null || !imagenes.Any())
                return BadRequest("Faltan datos del producto o imágenes");

            // Insertar producto
            var resultado = _dao.Insertar(producto);
            if (!resultado) return BadRequest("Error al insertar el producto");

            // Guardar imágenes
            for (int i = 0; i < imagenes.Count; i++)
            {
                var imagen = imagenes[i];
                if (imagen.Length > 0)
                {
                    var nombreArchivo = $"{Guid.NewGuid()}_{imagen.FileName}";
                    var rutaFisica = Path.Combine("wwwroot/imagenes", nombreArchivo);

                    using (var stream = new FileStream(rutaFisica, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // Agregar entrada a ImagenesProducto
                    var imagenProducto = new ImagenesProducto
                    {
                        ProductoId = producto.ProductoId, // ya fue insertado
                        UrlImagen = $"/imagenes/{nombreArchivo}",
                        EsPrincipal = (i == 0) // Marcar la primera como principal
                    };

                    _contexto.ImagenesProducto.Add(imagenProducto);
                }
            }
            Console.WriteLine($"Producto recibido: {producto.Nombre} - {producto.Precio}");
            Console.WriteLine($"Cantidad de imágenes recibidas: {imagenes.Count}");
            _contexto.SaveChanges();
            return Ok("Producto y sus imágenes guardados correctamente");
        }
        #endregion

        #region Actualizar producto
        [HttpPut("ActualizarProducto")]
        public IActionResult Actualizar([FromBody] Producto producto)
        {
            return _dao.Actualizar(producto) ? Ok("Producto actualizado") : NotFound("Producto no encontrado");
        }
        #endregion

        #region Eliminar producto
        [HttpDelete("EliminarporId/{id}")]
        public IActionResult Eliminar(int id)
        {
            return _dao.Eliminar(id) ? Ok("Producto eliminado") : NotFound("Producto no encontrado");
        }
        #endregion

        #region Obtener Productos para vista
        [HttpGet("conImagenPrincipal")]
        public IActionResult GetProductosConImagen()
        {
            var productos = _contexto.Productos
                .Where(p => p.Estado == true)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Nombre,
                    p.Precio,
                    Stock = p.Estado.HasValue && p.Estado.Value ? p.Stock : 0, // Si está activo, devuelve el stock; si no, 0
                    Imagen = _contexto.ImagenesProducto
                        .Where(img => img.ProductoId == p.ProductoId && img.EsPrincipal)
                        .Select(img => img.UrlImagen)
                        .FirstOrDefault()
                }).ToList();

            return Ok(productos);
        }
        #endregion

        [HttpGet("buscarConImagen/{nombre}")]
        public IActionResult BuscarConImagen(string nombre)
        {
            var productos = _contexto.Productos
                .Where(p => p.Nombre.Contains(nombre) && p.Estado == true)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Nombre,
                    p.Precio,
                    Stock = p.Estado.HasValue && p.Estado.Value ? p.Stock : 0,
                    Imagen = _contexto.ImagenesProducto
                        .Where(img => img.ProductoId == p.ProductoId && img.EsPrincipal)
                        .Select(img => img.UrlImagen)
                        .FirstOrDefault()
                }).ToList();

            return Ok(productos);
        }

        [HttpGet("filtrar")]
        public IActionResult FiltrarProductos([FromQuery] int? marcaId, [FromQuery] int? categoriaId, [FromQuery] float? precioMax)
        {
            var productos = _dao.GetByFiltros(marcaId, categoriaId, precioMax);
            var productosConImagen = productos.Select(p => new
            {
                p.ProductoId,
                p.Nombre,
                p.Precio,
                p.Stock,
                Imagen = _contexto.ImagenesProducto
                    .Where(img => img.ProductoId == p.ProductoId && img.EsPrincipal)
                    .Select(img => img.UrlImagen)
                    .FirstOrDefault()
            }).ToList();

            return Ok(productosConImagen);
        }

    }
}
