using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Repository
{
    public class ProductoDao
    {
        #region Contexto
        private readonly ProyectoProgra3Context contexto;

        public ProductoDao(ProyectoProgra3Context contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Obtener todos los productos
        public List<Producto> GetAll() => contexto.Productos.ToList();
        #endregion

        #region Obtener producto por ID
        public Producto? GetById(int id) => contexto.Productos.Find(id);
        #endregion

        #region Obtener productos por MarcaID
        public List<Producto> GetByMarca(int marcaId)
        {
            return contexto.Productos.Where(p => p.MarcaId == marcaId).ToList();
        }
        #endregion

        #region Obtener productos por UsuarioID (vía favoritos)
        public List<Producto> GetByUsuario(int usuarioId)
        {
            var favoritos = contexto.Favoritos.Where(f => f.UsuarioId == usuarioId).Select(f => f.ProductoId).ToList();
            return contexto.Productos.Where(p => favoritos.Contains(p.ProductoId)).ToList();
        }
        #endregion

        #region Obtener productos por nombre (búsqueda parcial)
        public List<Producto> GetByNombre(string nombre)
        {
            return contexto.Productos.Where(p => p.Nombre.Contains(nombre)).ToList();
        }
        #endregion

        #region Insertar producto
        public bool Insertar(Producto producto)
        {
            try
            {
                var nuevoProducto = new Producto
                {
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CategoriaId = producto.CategoriaId,
                    Sku = GenerarSku(),
                    Descripcion = producto.Descripcion,
                    MarcaId = producto.MarcaId,
                    ProveedorId = producto.ProveedorId,
                    Estado = producto.Estado
                };

                contexto.Productos.Add(nuevoProducto);
                contexto.SaveChanges();
                producto.ProductoId = nuevoProducto.ProductoId; // para usarlo si se necesita luego
                return true;
            }
            catch { return false; }
        }

        private string GenerarSku()
        {
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            int contador = 1;
            string sku;

            do
            {
                sku = $"PRD-{fecha}-{contador:D3}";
                contador++;
            } while (contexto.Productos.Any(p => p.Sku == sku));

            return sku;
        }
        #endregion

        #region Actualizar producto
        public bool Actualizar(Producto producto)
        {
            var productoExistente = GetById(producto.ProductoId);
            if (productoExistente == null) return false;

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;
            productoExistente.CategoriaId = producto.CategoriaId;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.MarcaId = producto.MarcaId;
            productoExistente.ProveedorId = producto.ProveedorId;
            productoExistente.Estado = producto.Estado;

            contexto.SaveChanges();
            return true;
        }
        #endregion

        #region Eliminar producto
        public bool Eliminar(int id)
        {
            var producto = GetById(id);
            if (producto == null) return false;

            // Obtener imágenes asociadas
            var imagenes = contexto.ImagenesProducto.Where(img => img.ProductoId == id).ToList();

            foreach (var img in imagenes)
            {
                var rutaFisica = Path.Combine("wwwroot", img.UrlImagen.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(rutaFisica))
                {
                    File.Delete(rutaFisica);
                }
            }

            // Eliminar registros de imágenes
            contexto.ImagenesProducto.RemoveRange(imagenes);

            // Eliminar el producto
            contexto.Productos.Remove(producto);

            contexto.SaveChanges();
            return true;
        }
        #endregion
    }
}