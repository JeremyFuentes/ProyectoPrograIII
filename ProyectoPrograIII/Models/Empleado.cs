using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Contacto { get; set; }

    public string? Correo { get; set; }

    public int? Edad { get; set; }

    public string? Dui { get; set; }

    public string? Horario { get; set; }

    public string? Cargo { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();
}
