using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorDao _dao;

        public AdministradorController(ProyectoProgra3Context contexto)
        {
            _dao = new AdministradorDao(contexto);
        }

        [HttpGet("todos")]
        public IActionResult GetAll() => Ok(_dao.GetAll());

        [HttpGet("ObtenerporId{id}")]
        public IActionResult GetById(int id)
        {
            var admin = _dao.GetById(id);
            return admin != null ? Ok(admin) : NotFound();
        }

        [HttpPost("crear")]
        public IActionResult Crear([FromBody] Administrador admin)
        {
            return _dao.Crear(admin) ? Ok("Administrador creado") : BadRequest("Error al crear");
        }

        [HttpPut("actualizar")]
        public IActionResult Actualizar([FromBody] Administrador admin)
        {
            return _dao.Actualizar(admin) ? Ok("Administrador actualizado") : NotFound("No encontrado");
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            return _dao.Eliminar(id) ? Ok("Administrador eliminado") : NotFound("No encontrado");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Administrador credenciales)
        {
            var admin = _dao.Login(credenciales.Correo, credenciales.ContrasenaHash);

            if (admin == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(new { idAdmin = admin.IdAdmin }); // 👈 solo devuelve el ID
        }
    }

}
