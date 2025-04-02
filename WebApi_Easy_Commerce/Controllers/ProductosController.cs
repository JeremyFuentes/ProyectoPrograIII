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
            foreach (var imagen in imagenes)
            {
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
                        EsPrincipal = false
                    };

                    _contexto.ImagenesProducto.Add(imagenProducto);
                }
            }

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
    }
}
