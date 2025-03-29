using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
