using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Repository
{
    public class FavoritoDao
    {
        #region Contexto
        private readonly ProyectoProgra3Context contexto = new ProyectoProgra3Context();
        #endregion

        #region Obtener todos los favoritos
        public List<Favorito> GetAll() => contexto.Favoritos.ToList();
        #endregion

        #region Obtener favorito por ID
        public Favorito? GetById(int id) => contexto.Favoritos.Find(id);
        #endregion

        #region Insertar favorito
        public bool Insertar(Favorito favorito)
        {
            try
            {
                contexto.Favoritos.Add(favorito);
                contexto.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region Eliminar favorito
        public bool Eliminar(int id)
        {
            var fav = GetById(id);
            if (fav == null) return false;

            contexto.Favoritos.Remove(fav);
            contexto.SaveChanges();
            return true;
        }
        #endregion

        #region Obtener favoritos por UsuarioId
        public List<Favorito> GetFavoritosPorUsuario(int usuarioId)
        {
            return contexto.Favoritos.Where(f => f.UsuarioId == usuarioId).ToList();
        }
        #endregion
    }
}
