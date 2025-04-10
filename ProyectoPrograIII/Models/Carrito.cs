using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPrograIII.Models
{
    public partial class Carrito
    {
        public int CarritoId { get; set; }

        public int? UsuarioId { get; set; }

        public int? ProductoId { get; set; }

        public int Cantidad { get; set; }

        public bool Comprado { get; set; }

        public double? PrecioUnitario { get; set; }

        public DateTime? FechaCompra { get; set; }

        public virtual Producto? Producto { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
