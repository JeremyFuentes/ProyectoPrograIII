using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Models
{
    [Table("Administradores")]
    public class Administrador
    {
        [Key]
        public int IdAdmin { get; set; }

        public string? Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Correo { get; set; }

        [Required]
        [MaxLength(255)]
        public string ContrasenaHash { get; set; }
    }
}
