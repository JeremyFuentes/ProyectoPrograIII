using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograIII.Repository
{
    public class CarritoDao
    {
        private readonly ProyectoProgra3Context _contexto;

        public CarritoDao(ProyectoProgra3Context contexto)
        {
            this._contexto = contexto;
        }

        public List<Carrito> GetCarritoPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId && !c.Comprado)
                .Include(c => c.Producto)
                .ThenInclude(p => p.ImagenesProducto)
                .ToList();
        }

        public List<Carrito> GetHistorialPorUsuario(int usuarioId)
        {
            return _contexto.Carrito
                .Where(c => c.UsuarioId == usuarioId && c.Comprado)
                .Include(c => c.Producto)
                .ToList();
        }

        public bool Agregar(Carrito carrito)
        {
            var existente = _contexto.Carrito.FirstOrDefault(c =>
                c.UsuarioId == carrito.UsuarioId &&
                c.ProductoId == carrito.ProductoId &&
                !c.Comprado
            );

            if (existente != null)
                return false;

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
            var carrito = _contexto.Carrito
                .FirstOrDefault(c => c.UsuarioId == usuarioId && c.ProductoId == productoId && !c.Comprado);

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
                .Include(c => c.Producto) // Incluimos la relación para acceder al stock
                .Where(c => c.UsuarioId == usuarioId && !c.Comprado)
                .ToListAsync();

            if (!items.Any()) return false;

            foreach (var item in items)
            {
                // Restar del stock si hay suficiente
                if (item.Producto.Stock >= item.Cantidad)
                {
                    item.Producto.Stock -= item.Cantidad;
                    item.Comprado = true;
                    item.FechaCompra = DateTime.Now;
                }
                else
                {
                    // Opcional: podrías lanzar una excepción o retornar false si no hay stock suficiente
                    throw new InvalidOperationException($"Stock insuficiente para el producto {item.Producto.Nombre}.");
                }
            }

            await _contexto.SaveChangesAsync();
            return true;
        }

    }
}
