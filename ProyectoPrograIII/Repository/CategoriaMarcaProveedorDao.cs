using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Repository
{
    public class CategoriaMarcaProveedorDao
    {
        private readonly ProyectoProgra3Context _contexto;

        public CategoriaMarcaProveedorDao(ProyectoProgra3Context contexto)
        {
            _contexto = contexto;
        }

        public List<Categorium> GetCategorias() => _contexto.Categoria.ToList();

        public List<Marca> GetMarcas() => _contexto.Marcas.ToList();

        public List<Proveedore> GetProveedores() => _contexto.Proveedores.ToList();
    }
}
