using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("favoritos")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly FavoritoDao _dao;

        public FavoritosController(ProyectoProgra3Context contexto)
        {
            _dao = new FavoritoDao(contexto);
        }

        #region Obtener todos los favoritos
        [HttpGet]
        public IActionResult GetAll() => Ok(_dao.GetAll());
        #endregion

        #region Obtener favorito por ID
        [HttpGet("obtenerFavorito/{id}")]
        public IActionResult GetById(int id)
        {
            var fav = _dao.GetById(id);
            return fav != null ? Ok(fav) : NotFound();
        }
        #endregion

        #region Obtener favoritos por usuario
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetPorUsuario(int usuarioId) => Ok(_dao.GetFavoritosPorUsuario(usuarioId));
        #endregion


        [HttpPost("Agregar")]
        public IActionResult AgregarAFavoritos([FromBody] Favorito favorito)
        {
            var resultado = _dao.Agregar(favorito); // Devuelve true/false

            if (resultado)
                return Ok(new { mensaje = "Agregado" });
            else
                return BadRequest(new { mensaje = "Error al agregar" });
        }

        [HttpDelete("Eliminar")]
        public IActionResult EliminarPorUsuarioYProducto([FromBody] Favorito favorito)
        {
            if (favorito.UsuarioId == null || favorito.ProductoId == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            var resultado = _dao.EliminarPorUsuarioYProducto(favorito.UsuarioId.Value, favorito.ProductoId.Value);

            return resultado
                ? Ok(new { mensaje = "Favorito eliminado" })
                : NotFound(new { mensaje = "Favorito no encontrado" });
        }

        #region Eliminar favorito
        [HttpDelete("Eliminarfavorito/{id}")]
        public IActionResult Eliminar(int id)
        {
            return _dao.Eliminar(id)
    ? Ok(new { mensaje = "Favorito eliminado" })
    : NotFound(new { mensaje = "Favorito no encontrado" });
        }
        #endregion

        [HttpGet("ConProductoPorUsuario/{usuarioId}")]
        public IActionResult GetFavoritosConProductoPorUsuario(int usuarioId)
        {
            try
            {
                Console.WriteLine($"🔍 Obteniendo favoritos del usuario {usuarioId}");
                var favoritos = _dao.GetFavoritosConProductoPorUsuario(usuarioId);
                Console.WriteLine($"✅ Se encontraron {favoritos.Count} favoritos.");
                return Ok(favoritos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en el controller: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno", detalle = ex.Message });
            }
        }
    }


}
