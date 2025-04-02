using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Models
{
    public class ImagenesProducto
    {
        public int ImagenId { get; set; }
        public int ProductoId { get; set; }
        public string UrlImagen { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsPrincipal { get; set; }

        // Relación con Producto
        public Producto? Producto { get; set; }
    }
}
