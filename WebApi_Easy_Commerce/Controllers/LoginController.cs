using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;
using Google.Apis.Auth;
using ProyectoPrograIII.Context;
using Microsoft.SqlServer.Server;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginDao _loginDao = new LoginDao();

        private readonly ProyectoProgra3Context _context;

        public LoginController(ProyectoProgra3Context context)
        {
            _context = context;
        }


        #region Seleecionar Todos
        [HttpGet("ObtenerTodosUsuarios")]
        public IActionResult ObtenerTodosLosUsuarios()
        {
            var usuarios = _loginDao.getAll();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound("No hay usuarios registrados.");
            }

            return Ok(usuarios);
        }
        #endregion

        #region ObtenerPorId
        [HttpGet("ObtenerUsuarioPorId")]
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

            return Ok(new { usuario.Correo, usuario.UsuarioId});
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

        #region Autentificacion Login
        [HttpPost("AutenticarUsuario")]
        public IActionResult AutenticarUsuario([FromBody] Usuario usuarioLogin)
        {
            var usuario = _loginDao.login(usuarioLogin.Correo, usuarioLogin.Contraseña);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok(new
            {
                idUsuario = usuario.UsuarioId,
                nombre = usuario.Nombre
            });
        }
        #endregion

        #region Validar Direccion y Contacto
        [HttpGet("ValidarDireccionContacto/{id}")]
        public IActionResult ValidarDireccionContacto(int id)
        {
            try
            {
                var (valido, camposFaltantes) = _loginDao.ValidarDireccionYContacto(id);

                if (!valido)
                {
                    return BadRequest(new
                    {
                        mensaje = "Debe completar los siguientes campos para continuar.",
                        camposFaltantes
                    });
                }

                return Ok(new { mensaje = "Verificación exitosa." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        #endregion

        #region LoginGoogle
        [HttpPost("AutenticarGoogle")]
        public async Task<IActionResult> AutenticarConGoogle([FromBody] string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                var usuario = _loginDao.AutenticarConGoogle(
                    payload.Subject,   // GoogleId
                    payload.Email,
                    payload.Name
                );

                return Ok(new
                {
                    mensaje = "Autenticación con Google exitosa",
                    usuarioId = usuario.UsuarioId,
                    nombre = usuario.Nombre,
                    correo = usuario.Correo,
                    metodo = usuario.MetodoLogin
                });
            }
            catch (InvalidJwtException ex)
            {
                return Unauthorized(new { mensaje = "Token inválido o expirado", error = ex.Message });
            }
            #endregion
        }
    }
}
