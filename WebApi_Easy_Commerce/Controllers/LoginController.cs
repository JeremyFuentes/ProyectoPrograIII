using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginDao _loginDao = new LoginDao();

        #region Seleecionar Todos
        [HttpGet("ObtenerTodosUsuarios")]

        public Usuario SelectAll(Usuario alumno)
        {
            return alumno ;
        }
        #endregion

        #region ObtenerPorId
        [HttpGet("ObetenrUsuarioPorId")]
        public Usuario seletById(int id)
        {
            var alumno = _loginDao.GetById(id);
            return alumno;
        }
        #endregion

        #region ObtenerPorCorreo
        [HttpGet("ObtenerUsuarioPorCorreo")]
        public IActionResult GetByCorreo(string correo)
        {
            var usuario = _loginDao.GetByCorreo(correo);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok(new { usuario.Correo, usuario.Contraseña });
        }
        #endregion

        #region Crear Usuario
        [HttpPost("CrearUsuario")]
        public IActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contraseña) || string.IsNullOrEmpty(usuario.Correo))
            {
                return BadRequest("Datos inválidos. Nombre, Correo y Contraseña son obligatorios.");
            }

            var creado = _loginDao.CrearUsuario(usuario);

            if (creado)
            {
                return Ok("Usuario creado exitosamente");
            }

            return Conflict("El correo ya está registrado. Usa otro correo o inicia sesión.");
        }
        #endregion

        #region Actualizar
        [HttpPut("ActualizarUsuario")]
        public bool actualizarAlumno([FromBody] Usuario alumno)
        {
            return _loginDao.actualizar(alumno.UsuarioId, alumno);
        }
        #endregion

        #region Eliminar
        [HttpDelete("EliminarUsuario")]
        public bool eliminarAlumno(int id)
        {
            return _loginDao.eliminarUsuario(id);
        }
        #endregion

    }
}
