using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Models
{
    [Table("ImagenesProducto")]
    public class ImagenesProducto
    {
        [Key] // 🔥 ESTA LÍNEA es crucial
        public int IdImagen { get; set; }

        public int ProductoId { get; set; }

        [MaxLength(500)]
        public string? UrlImagen { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public bool EsPrincipal { get; set; }
    }
}
