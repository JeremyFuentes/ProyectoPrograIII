using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Proveedore
{
    public int ProveedorId { get; set; }

    public string? Nombre { get; set; }

    public string? Contacto { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
