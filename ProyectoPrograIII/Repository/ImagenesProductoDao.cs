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
        private readonly ProyectoProgra3Context contexto = new ProyectoProgra3Context();
        #endregion

        #region Obtener imágenes por ProductoID
        public List<ImagenesProducto> GetByProducto(int productoId)
        {
            return contexto.ImagenesProducto.Where(i => i.ProductoId == productoId).ToList();
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
    }

}
