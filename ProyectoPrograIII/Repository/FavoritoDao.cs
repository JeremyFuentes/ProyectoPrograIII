using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ProyectoProgra3Context contexto;

        public FavoritoDao(ProyectoProgra3Context contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Obtener todos los favoritos
        public List<Favorito> GetAll() => contexto.Favoritos.ToList();
        #endregion

        #region Obtener favorito por ID
        public Favorito? GetById(int id) => contexto.Favoritos.Find(id);
        #endregion

        #region Obtener favoritos por UsuarioId
        public List<Favorito> GetFavoritosPorUsuario(int usuarioId)
        {
            return contexto.Favoritos.Where(f => f.UsuarioId == usuarioId).ToList();
        }
        #endregion

        #region Obtener favoritos con detalle de producto por UsuarioId
        public List<Favorito> GetFavoritosConProductoPorUsuario(int usuarioId)
        {
            return contexto.Favoritos
                .Where(f => f.UsuarioId == usuarioId)
                .Select(f => new Favorito
                {
                    FavoritoId = f.FavoritoId,
                    UsuarioId = f.UsuarioId,
                    ProductoId = f.ProductoId,
                    Producto = new Producto
                    {
                        ProductoId = f.Producto.ProductoId,
                        Nombre = f.Producto.Nombre,
                        Precio = f.Producto.Precio,
                        ImagenesProducto = f.Producto.ImagenesProducto
                            .Where(img => img.EsPrincipal)
                            .ToList()
                    }
                })
                .ToList();
        }

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

        public bool Agregar(Favorito favorito)
        {
            try
            {
                // Verifica si ya existe este favorito
                bool yaExiste = contexto.Favoritos.Any(f =>
                    f.UsuarioId == favorito.UsuarioId &&
                    f.ProductoId == favorito.ProductoId);

                if (yaExiste) return false;

                contexto.Favoritos.Add(favorito);
                contexto.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

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

        public bool EliminarPorUsuarioYProducto(int usuarioId, int productoId)
        {
            var favorito = contexto.Favoritos
                .FirstOrDefault(f => f.UsuarioId == usuarioId && f.ProductoId == productoId);

            if (favorito == null) return false;

            contexto.Favoritos.Remove(favorito);
            contexto.SaveChanges();
            return true;
        }


    }
}
