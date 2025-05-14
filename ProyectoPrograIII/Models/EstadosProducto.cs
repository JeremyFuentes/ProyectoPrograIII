using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Models
{
    [Table("EstadosProducto")]
    public class EstadosProducto
    {
        [Key]
        public int EstadoProductoId { get; set; }

        [Column("NombreEstado")]
        public string Nombre { get; set; }

        public ICollection<Carrito> Carritos { get; set; }
    }
}
