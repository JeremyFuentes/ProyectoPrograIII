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
        public IActionResult GetPorUsuario(int usuarioId) =>
            Ok(_dao.GetCarritoPorUsuario(usuarioId));

        [HttpGet("historial/{usuarioId}")]
        public IActionResult GetHistorial(int usuarioId) =>
            Ok(_dao.GetHistorialPorUsuario(usuarioId));

        [HttpPost("agregar")]
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
            bool ok = _dao.ActualizarCantidad(carritoId, cantidad);
            return ok ? Ok() : NotFound();
        }

        [HttpPut("confirmarCompra/{usuarioId}")]
        public async Task<IActionResult> ConfirmarCompra(int usuarioId)
        {
            try
            {
                bool result = await _dao.ConfirmarCompra(usuarioId);
                return result ? Ok() : NotFound();
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

            bool ok = _dao.EliminarPorUsuarioYProducto(carrito.UsuarioId.Value, carrito.ProductoId.Value);
            return ok ? Ok(new { mensaje = "Eliminado correctamente" }) : NotFound();
        }

        [HttpGet("activos/{usuarioId}")]
        public IActionResult GetPedidosActivos(int usuarioId) =>
    Ok(_dao.GetPedidosActivosPorUsuario(usuarioId));

        [HttpGet("entregados/{usuarioId}")]
        public IActionResult GetPedidosEntregados(int usuarioId) =>
            Ok(_dao.GetHistorialEntregadosPorUsuario(usuarioId));

        [HttpGet("PedidosAgrupados")]
        public IActionResult PedidosAgrupados()
        {
            var pedidos = _dao.ObtenerPedidosAgrupados();
            return Ok(pedidos);
        }

        [HttpPut("ActualizarEstado/{carritoId}/{nuevoEstadoId}")]
        public IActionResult ActualizarEstado(int carritoId, int nuevoEstadoId)
        {
            var exito = _dao.ActualizarEstado(carritoId, nuevoEstadoId);
            if (!exito) return NotFound();
            return Ok();
        }
    }
}
