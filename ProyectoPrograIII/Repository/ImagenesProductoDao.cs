using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Repository
{
    public class ImagenesProductoDao
    {
        #region Contexto
        private readonly ProyectoProgra3Context contexto;

        public ImagenesProductoDao(ProyectoProgra3Context contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Obtener imágenes por ProductoID
        public List<ImagenesProducto> GetByProducto(int productoId)
        {
            return contexto.ImagenesProducto.Where(i => i.ProductoId == productoId).ToList();
        }
        #endregion

        #region Obtener imagen por ID
        public ImagenesProducto? GetById(int id)
        {
            return contexto.ImagenesProducto.FirstOrDefault(i => i.IdImagen == id);
        }
        #endregion

        #region Insertar imagen
        public bool Insertar(ImagenesProducto imagen)
        {
            try
            {
                contexto.ImagenesProducto.Add(imagen);
                contexto.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region Eliminar imagen por ID
        public bool Eliminar(int id)
        {
            var imagen = contexto.ImagenesProducto.Find(id);
            if (imagen == null) return false;

            contexto.ImagenesProducto.Remove(imagen);
            contexto.SaveChanges();
            return true;
        }
        #endregion

        #region Marcar como principal
        public bool MarcarComoPrincipal(int productoId, int nuevaPrincipalId)
        {
            var imagenes = contexto.ImagenesProducto.Where(i => i.ProductoId == productoId).ToList();
            if (!imagenes.Any()) return false;

            foreach (var img in imagenes)
                img.EsPrincipal = img.IdImagen == nuevaPrincipalId;

            contexto.SaveChanges();
            return true;
        }
        #endregion
    }
}
