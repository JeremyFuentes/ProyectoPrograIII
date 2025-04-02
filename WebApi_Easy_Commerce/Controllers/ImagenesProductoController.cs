using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("imagenesProducto")]
    [ApiController]
    public class ImagenesProductoController : ControllerBase
    {
        private readonly ImagenesProductoDao _dao = new ImagenesProductoDao();

        #region Obtener imágenes por producto
        [HttpGet("producto/{productoId}")]
        public IActionResult GetByProducto(int productoId)
        {
            var imagenes = _dao.GetByProducto(productoId);
            return imagenes != null && imagenes.Count > 0 ? Ok(imagenes) : NotFound("No se encontraron imágenes");
        }
        #endregion

        #region Insertar imagen
        [HttpPost("InsertarImagen")]
        public IActionResult Insertar([FromBody] ImagenesProducto imagen)
        {
            return _dao.Insertar(imagen) ? Ok("Imagen insertada") : BadRequest("Error al insertar");
        }
        #endregion

        #region Eliminar imagen
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            return _dao.Eliminar(id) ? Ok("Imagen eliminada") : NotFound("Imagen no encontrada");
        }
        #endregion
    }
}
