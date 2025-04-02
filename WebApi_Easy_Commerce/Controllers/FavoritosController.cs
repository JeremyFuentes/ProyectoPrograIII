using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("favoritos")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly FavoritoDao _dao = new FavoritoDao();

        #region Obtener todos los favoritos
        [HttpGet]
        public IActionResult GetAll() => Ok(_dao.GetAll());
        #endregion

        #region Obtener favorito por ID
        [HttpGet("ObtenerporID/{id}")]
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

        #region Insertar favorito
        [HttpPost]
        public IActionResult Insertar([FromBody] Favorito favorito)
        {
            return _dao.Insertar(favorito) ? Ok("Favorito agregado") : BadRequest("Error al insertar");
        }
        #endregion

        #region Eliminar favorito
        [HttpDelete("EliminarporId/{id}")]
        public IActionResult Eliminar(int id)
        {
            return _dao.Eliminar(id) ? Ok("Favorito eliminado") : NotFound("Favorito no encontrado");
        }
        #endregion
    }
}
