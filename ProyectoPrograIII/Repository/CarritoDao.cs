using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograIII.Repository
{
    public class CarritoDao
    {
        private readonly ProyectoProgra3Context _contexto;
        private const int ESTADO_PENDIENTE = 1;
        private const int ESTADO_TRANSACCION = 2;
        private const int ESTADO_ENTREGADO = 5;

        public CarritoDao(ProyectoProgra3Context contexto)
        {
            _contexto = contexto;
        }

        public List<Carrito> GetCarritoPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId && c.EstadoProductoId == ESTADO_PENDIENTE)
                .Include(c => c.Producto)
                    .ThenInclude(p => p.ImagenesProducto)
                .ToList();
        }

        public List<Carrito> GetHistorialPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId && c.EstadoProductoId != ESTADO_PENDIENTE)
                .Include(c => c.Producto)
                .ToList();
        }

        public List<Carrito> GetPedidosActivosPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId &&
                            c.EstadoProductoId != ESTADO_PENDIENTE &&
                            c.EstadoProductoId != ESTADO_ENTREGADO)
                .Include(c => c.Producto)
                .ThenInclude(p => p.ImagenesProducto)
                .Include(c => c.EstadoProducto)
                .ToList();
        }

        public List<Carrito> GetHistorialEntregadosPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId &&
                            c.EstadoProductoId == ESTADO_ENTREGADO)
                .Include(c => c.Producto)
                .ThenInclude(p => p.ImagenesProducto)
                .Include(c => c.EstadoProducto)
                .ToList();
        }


        public bool Agregar(Carrito carrito)
        {
            var existente = _contexto.Carrito.FirstOrDefault(c =>
                c.UsuarioId == carrito.UsuarioId &&
                c.ProductoId == carrito.ProductoId &&
                c.EstadoProductoId == ESTADO_PENDIENTE  // 🟢 Estado pendiente de pago
            );

            if (existente != null) return false;

            carrito.EstadoProductoId = ESTADO_PENDIENTE;

            _contexto.Carrito.Add(carrito);
            _contexto.SaveChanges();
            return true;
        }

        public bool EliminarPorId(int id)
        {
            var carrito = _contexto.Carrito.Find(id);
            if (carrito == null) return false;

            _contexto.Carrito.Remove(carrito);
            _contexto.SaveChanges();
            return true;
        }

        public bool EliminarPorUsuarioYProducto(int usuarioId, int productoId)
        {
            var carrito = _contexto.Carrito.FirstOrDefault(c =>
                c.UsuarioId == usuarioId && c.ProductoId == productoId && c.EstadoProductoId == ESTADO_PENDIENTE);

            if (carrito == null) return false;

            _contexto.Carrito.Remove(carrito);
            _contexto.SaveChanges();
            return true;
        }

        public bool ActualizarCantidad(int carritoId, int cantidad)
        {
            var carrito = _contexto.Carrito.Find(carritoId);
            if (carrito == null) return false;

            carrito.Cantidad = cantidad;
            _contexto.SaveChanges();
            return true;
        }

        public async Task<bool> ConfirmarCompra(int usuarioId)
        {
            var items = await _contexto.Carrito
                .Include(c => c.Producto)
                .Where(c => c.UsuarioId == usuarioId && c.EstadoProductoId == ESTADO_PENDIENTE)
                .ToListAsync();

            if (!items.Any()) return false;

            foreach (var item in items)
            {
                if (item.Producto.Stock >= item.Cantidad)
                {
                    item.Producto.Stock -= item.Cantidad;
                    item.EstadoProductoId = ESTADO_TRANSACCION;

                    item.FechaCompra = DateTime.Now; // ✅ Aquí se registra la fecha real de compra
                }
                else
                {
                    throw new InvalidOperationException($"Stock insuficiente para {item.Producto.Nombre}.");
                }
            }


            await _contexto.SaveChangesAsync();
            return true;
        }

        public List<object> ObtenerPedidosAgrupados()
        {
            return _contexto.Carrito
                .Include(c => c.Usuario)
                .Include(c => c.Producto)
                .Where(c => c.EstadoProductoId > 1) // solo pedidos en proceso o más
                .GroupBy(c => c.Usuario)
                .Select(g => new
                {
                    nombre = g.Key.Nombre,
                    correo = g.Key.Correo,
                    pedidos = g.Select(p => new
                    {
                        carritoId = p.CarritoId,
                        nombreProducto = p.Producto.Nombre,
                        estadoProductoId = p.EstadoProductoId
                    }).ToList()
                }).ToList<object>();
        }

        public bool ActualizarEstado(int carritoId, int nuevoEstadoId)
        {
            var pedido = _contexto.Carrito.FirstOrDefault(c => c.CarritoId == carritoId);
            if (pedido == null) return false;

            pedido.EstadoProductoId = nuevoEstadoId;
            _contexto.SaveChanges();
            return true;
        }
    }
}
