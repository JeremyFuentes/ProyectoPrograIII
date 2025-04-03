using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;
using System.IO;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("imagenesProducto")]
    [ApiController]
    public class ImagenesProductoController : ControllerBase
    {
        private readonly ImagenesProductoDao _dao;
        private readonly ProyectoProgra3Context _context;

        public ImagenesProductoController(ProyectoProgra3Context contexto)
        {
            _context = contexto;
            _dao = new ImagenesProductoDao(contexto);
        }

        #region Obtener imágenes por producto
        [HttpGet("producto/{productoId}")]
        public IActionResult GetByProducto(int productoId)
        {
            var imagenes = _dao.GetByProducto(productoId);
            return imagenes != null && imagenes.Count > 0 ? Ok(imagenes) : NotFound("No se encontraron imágenes");
        }
        #endregion

        #region Insertar imagen
        [HttpPost("insertarImagen")]
        public IActionResult Insertar([FromBody] ImagenesProducto imagen)
        {
            return _dao.Insertar(imagen) ? Ok("Imagen insertada") : BadRequest("Error al insertar");
        }
        #endregion

        #region Subir múltiples imágenes (para modo edición)
        [HttpPost("subirVarias")]
        public async Task<IActionResult> SubirVarias([FromForm] int productoId, List<IFormFile> imagenes)
        {
            if (!imagenes.Any()) return BadRequest("No se enviaron imágenes");

            foreach (var img in imagenes)
            {
                var nombreArchivo = $"{Guid.NewGuid()}_{img.FileName}";
                var rutaFisica = Path.Combine("wwwroot/imagenes", nombreArchivo);

                using (var stream = new FileStream(rutaFisica, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                var nueva = new ImagenesProducto
                {
                    ProductoId = productoId,
                    UrlImagen = $"/imagenes/{nombreArchivo}",
                    EsPrincipal = false
                };
                _context.ImagenesProducto.Add(nueva);
            }
            _context.SaveChanges();
            return Ok("Imágenes añadidas correctamente");
        }
        #endregion

        #region Eliminar imagen
        [HttpDelete("eliminarImagen/{id}")]
        public IActionResult Eliminar(int id)
        {
            var imagen = _context.ImagenesProducto.FirstOrDefault(i => i.IdImagen == id);
            if (imagen == null) return NotFound();

            var ruta = Path.Combine("wwwroot", imagen.UrlImagen.TrimStart('/'));
            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }

            // Si era principal, no hay ninguna principal ahora
            if (imagen.EsPrincipal)
            {
                imagen.EsPrincipal = false;
            }

            _context.ImagenesProducto.Remove(imagen);
            _context.SaveChanges();

            return Ok("Imagen eliminada correctamente");
        }
        #endregion

        #region Marcar como principal
        [HttpPost("marcarComoPrincipal/{idImagen}")]
        public IActionResult MarcarComoPrincipal(int idImagen)
        {
            var imagen = _dao.GetById(idImagen);
            if (imagen == null) return NotFound();

            var resultado = _dao.MarcarComoPrincipal(imagen.ProductoId, idImagen);
            return resultado ? Ok() : BadRequest("No se pudo marcar como principal");
        }
        #endregion

        [HttpPut("establecerPrincipal/{idImagen}")]
        public IActionResult EstablecerPrincipal(int idImagen)
        {
            var imagen = _context.ImagenesProducto.FirstOrDefault(i => i.IdImagen == idImagen);
            if (imagen == null) return NotFound();

            var todas = _context.ImagenesProducto.Where(i => i.ProductoId == imagen.ProductoId).ToList();
            foreach (var img in todas)
                img.EsPrincipal = img.IdImagen == idImagen;

            _context.SaveChanges();
            return Ok("Imagen marcada como principal");
        }
    }
}
