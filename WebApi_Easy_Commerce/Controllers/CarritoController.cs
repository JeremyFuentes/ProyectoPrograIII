using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using ProyectoPrograIII.Repository;

namespace ProyectoPrograIII.Controllers
{
    [Route("carrito")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoDao _dao;

        public CarritoController(ProyectoProgra3Context contexto)
        {
            _dao = new CarritoDao(contexto);
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetPorUsuario(int usuarioId) => Ok(_dao.GetCarritoPorUsuario(usuarioId));

        [HttpGet("historial/{usuarioId}")]
        public IActionResult GetHistorial(int usuarioId) => Ok(_dao.GetHistorialPorUsuario(usuarioId));

        [HttpPost("Agregar")]
        public IActionResult AgregarProducto([FromBody] Carrito item)
        {
            bool agregado = _dao.Agregar(item);

            if (!agregado)
                return Conflict(new { mensaje = "El producto ya está en el carrito." });

            return Ok(new { mensaje = "Producto agregado." });
        }


        [HttpPut("actualizarCantidad")]
        public IActionResult ActualizarCantidad([FromQuery] int carritoId, [FromQuery] int cantidad)
        {
            var ok = _dao.ActualizarCantidad(carritoId, cantidad);
            return ok ? Ok() : NotFound();
        }

        [HttpPut("ConfirmarCompra/{usuarioId}")]
        public async Task<IActionResult> ConfirmarCompra(int usuarioId)
        {
            try
            {
                var result = await _dao.ConfirmarCompra(usuarioId);
                if (result)
                    return Ok();
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpDelete("eliminar/{carritoId}")]
        public IActionResult Eliminar(int carritoId)
        {
            return _dao.EliminarPorId(carritoId)
                ? Ok(new { mensaje = "Producto eliminado del carrito" })
                : NotFound(new { mensaje = "No encontrado" });
        }

        [HttpDelete("eliminarProducto")]
        public IActionResult EliminarPorUsuarioYProducto([FromBody] Carrito carrito)
        {
            if (carrito.UsuarioId == null || carrito.ProductoId == null)
                return BadRequest(new { mensaje = "Datos incompletos" });

            var ok = _dao.EliminarPorUsuarioYProducto(carrito.UsuarioId.Value, carrito.ProductoId.Value);
            return ok ? Ok(new { mensaje = "Eliminado correctamente" }) : NotFound();
        }
    }
}
